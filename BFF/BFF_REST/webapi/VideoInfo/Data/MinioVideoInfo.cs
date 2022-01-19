using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel;
using WebApi.Models;

namespace WebApi.Data
{
    public class MinioVideoInfo : IVideoInfo
    {
        private readonly MinioClient _minio;
        private const string scp_bucket = "scp-bucket";
        private const string face_bucket = "face-bucket";

        public MinioVideoInfo(MinioClient minio)
        {
            _minio = minio ?? throw new ArgumentException(nameof(minio));
        }

        public async Task<IEnumerable<VideoInfo>> GetAllVideosAsync()
        {
            Console.WriteLine("Minio testing");

            List<VideoInfo> videoList = new List<VideoInfo>();

            var scpVideos = await GetVideoList(scp_bucket);
            Console.WriteLine(scpVideos.Count());
            for (int i = 0; i < scpVideos.Count(); ++i)
            {
                VideoInfo videoInfo = new VideoInfo();
                var obj = scpVideos[i];
                videoInfo.Id = i;
                videoInfo.Name = obj.Key;
                videoInfo.Url1 = await _minio.PresignedGetObjectAsync(scp_bucket, obj.Key, 60 * 60 * 24);
                videoInfo.Url2 = "";
                videoInfo.timestamp = obj.LastModified;

                Console.WriteLine("all: "+scp_bucket + " " + videoInfo.Name);
                videoList.Add(videoInfo);
            }

            var faceVideos = await GetVideoList(face_bucket);
            if (faceVideos.Count() != scpVideos.Count())
            {
                Console.WriteLine("video is not match");
                return null;
            }

            foreach (var faceVideo in faceVideos)
            {
                foreach (var video in videoList)
                {
                    if (video.Name == faceVideo.Key)
                    {
                        video.Url2 = await _minio.PresignedGetObjectAsync(face_bucket, faceVideo.Key, 60 * 60 * 24 * 7);
                    }
                }
            }

            Console.WriteLine(videoList.Count());
            return videoList;
        }

        public async Task<List<Item>> GetVideoList(string bucketName, string prefix = null, bool recursive = true)
        {
            try
            {
                bool found = await _minio.BucketExistsAsync(bucketName);

                if (found)
                {
                    Console.WriteLine($"{bucketName} exist.");
                    IObservable<Item> observable = _minio.ListObjectsAsync(bucketName, prefix, recursive);

                    var items = observable.ToEnumerable();

                    var bucketKeys = from item in items
                                     orderby item.LastModified descending
                                     select item;

                    Console.WriteLine(bucketKeys.ToList().Count());

                    return bucketKeys.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}
