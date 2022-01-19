using System.Net.Mime;
using WebApi.Data;
using WebApi.Models;
using WebApi.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    // [Route("api/[controller]")]    //api/deviceMsgInfo
    [Route("api/deviceMsg")]
    [ApiController]
    public class DeviceMsgInfoController: ControllerBase
    {
        private readonly IDeviceMsgInfoRepo _repository;
        private readonly IMapper _mapper;

        public DeviceMsgInfoController(IDeviceMsgInfoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/deviceMsg
        [HttpGet]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<AllDeviceMsgReadDto>>> GetAllDeviceMsgInfosAsync()
        {
            var deviceMsgItems = await _repository.GetAllDeviceMsgInfosAsync();

            if(deviceMsgItems == null){
                return NotFound("NotFound");
            }
            return Ok(_mapper.Map<IEnumerable<AllDeviceMsgReadDto>>(deviceMsgItems));
        }

        //GET api/deviceMsg/agentList
        [HttpGet("agentList")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<string>>> GetAgentListAsync()
        {
            var agents = await _repository.GetAgentListAsync();

            if(agents == null)
            {
                return NotFound("NotFound");
            }

            return Ok(agents);
        }

        //GET api/deviceMsg/deviceList
        [HttpGet("deviceList")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<string>>> GetDeviceListAsync()
        {
            var devices = await _repository.GetDeviceListAsync();

            if(devices == null)
            {
                return NotFound("NotFound");
            }

            return Ok(devices);
        }

        //GET api/deviceMsg/deviceList/{agentId}
        [HttpGet("deviceList/{agentId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<string>>> GetDeviceListByAgentAsync(string agentId)
        {
            var devices = await _repository.GetDeviceListByAgentAsync(agentId);

            if(devices == null)
            {
                return NotFound("NotFound");
            }

            return Ok(devices);
        }

        //GET api/deviceMsg/id/{deviceId}
        [HttpGet("id/{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<DeviceMsgInfoReadDto>>> GetDeviceMsgInfoByIdAsync(string deviceId)
        {
            var deviceMsgItems = await _repository.GetDeviceMsgInfoByIdAsync(deviceId);

            if(deviceMsgItems == null)
            {
                return NotFound("NotFound");
            }

            return Ok(_mapper.Map<IEnumerable<DeviceMsgInfoReadDto>>(deviceMsgItems));
        }

        //Get api/deviceMsg/newest/{deviceId}
        [HttpGet("newest/{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<DeviceMsgInfoReadDto>> GetNewestMsgInfoAsync(string deviceId)
        {
            var deviceMsgItem = await _repository.GetNewestMsgInfoAsync(deviceId);

            if(deviceMsgItem == null)
            {
                return NotFound("NotFound");
            }

            return Ok(_mapper.Map<DeviceMsgInfoReadDto>(deviceMsgItem));
        }

        //GET api/deviceMsg/timestamp/{ts}
        [HttpGet("timestamp/{ts}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<DeviceMsgInfoReadDto>> GetDeviceMsgInfoByTimeAsync(string ts)
        {
            var deviceMsgItem = await _repository.GetDeviceMsgInfoByTimeAsync(ts);
            if(deviceMsgItem == null)
            {
               return NotFound("NotFound");             
            }
            return Ok(_mapper.Map<DeviceMsgInfoReadDto>(deviceMsgItem));
        }

        //Delete api/deviceMsg/{recordId}
        [HttpDelete("recordId")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> DeleteDeviceMsgInfo(string recordId)
        {
            var deviceMsgInfoItem = await _repository.GetDeviceMsgInfoByRecordAsync(recordId);
            if(deviceMsgInfoItem == null)
            {
                return NotFound("NotFound");
            }

            _repository.DeleteDeviceMsgInfo(deviceMsgInfoItem);
            await _repository.SaveChangesAsync();

            return Ok("Ok");
        }
    
        //PUT api/deviceMsg/{recordId}
        [HttpPut("recordId")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> UpdateDeviceMsgInfoAsync(string recordId, DeviceMsgInfoUpdate deviceMsgInfoUpdate)
        {
            var dmi = _mapper.Map<DeviceMsgInfo>(deviceMsgInfoUpdate);
            if(dmi == null)
            {
                return BadRequest("Input Error");
            }
            await _repository.UpdateDeviceMsgInfoAsync(dmi);
            
            return Ok("Ok");
        }
    }
}