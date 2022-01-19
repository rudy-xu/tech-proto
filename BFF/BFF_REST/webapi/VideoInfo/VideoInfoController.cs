using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using WebApi.Models;
using WebApi.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoInfoController: ControllerBase
    {
        private readonly IVideoInfo _repository;

        public VideoInfoController(IVideoInfo repository)
        {
            _repository = repository;
        }

        //GET: api/videoInfo
        [HttpGet]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<List<VideoInfo>>> GetAllVideos()
        {
            var videoItems = await _repository.GetAllVideosAsync();

            if(videoItems == null)
            {
                return NotFound("NotFound");
            }
            return Ok(videoItems);
        }
    }    
}