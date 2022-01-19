using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IVideoInfo
    {
        Task<IEnumerable<VideoInfo>> GetAllVideosAsync();

        // Task<VideoInfo> GetNewsVideoAsync();
    }
}