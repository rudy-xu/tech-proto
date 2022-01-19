using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using WebApi.Data;
using WebApi.Models;
using WebApi.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    //api/[controller] -- [controller] --> the prefix of DeviceStatusController: DeviceStatus
    //api/DeviceStatus
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceStatusController : ControllerBase
    {
        private readonly IDeviceStatusRepo _repository;
        private readonly IMapper _mapper;

        public DeviceStatusController(IDeviceStatusRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/deviceStatus
        [HttpGet]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<DeviceStaReadDto>>> GetAllDeviceStatusesAsync()
        {
            var deviceStatusItems = await _repository.GetAllDeviceStatusesAsync();

            if (deviceStatusItems == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<DeviceStaReadDto>>(deviceStatusItems));
        }

        //GET api/deviceStatus/id/1011
        [HttpGet("id/{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<DeviceStaReadDto>> GetDeviceStatusByIdAsync(string deviceId)
        {
            var deviceStatusItem = await _repository.GetDeviceStatusByIdAsync(deviceId);

            if (deviceStatusItem == null)
            {
                return NotFound("not found");
            }

            return Ok(_mapper.Map<DeviceStaReadDto>(deviceStatusItem));
        }

        //GET api/deviceStatus/timestamp/{ts}
        [HttpGet("timestamp/{ts}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<DeviceStaReadDto>> GetDeviceStatusByTimeAsync(string ts)
        {
            var deviceStatusItem = await _repository.GetDeviceStatusByTimeAsync(ts);

            if (deviceStatusItem == null)
            {
                return NotFound("not found");
            }

            return Ok(_mapper.Map<DeviceStaReadDto>(deviceStatusItem));
        }

        //POST api/deviceStatus
        [HttpPost]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> AddDeviceStatusAsync(DeviceStaCreateDto deviceStaCreateDto)
        {
            var deviceStatusItem = _mapper.Map<DeviceStatus>(deviceStaCreateDto);

            if(deviceStatusItem == null)
            {
                return BadRequest("Input error");
            }
            await _repository.AddDeviceStatusAsync(deviceStatusItem);

            return Ok("Ok");
        }

        //PUT api/deviceStatus/{deviceId}
        /*
            {
                "deviceID": "1014",
                "status": 0,
                "timeStamp": "2021-12-18T12:23:44.111Z"
            }
        */
        [HttpPut("{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> UpdateDeviceStatusAsync(string deviceId, DeviceStaUpdateDto deviceStaUpdateDto)
        {
            var deviceStatusItem = _mapper.Map<DeviceStatus>(deviceStaUpdateDto);
            
            if(deviceStatusItem == null)
            {
                return BadRequest("Input error");
            }

            await _repository.UpdateDeviceStatusAsync(deviceStatusItem);

            return Ok("Ok");
        }

        //PATCH api/deviceStatus/{deviceId}
        /* Body
            [
                {
                "op": "replace",
                "path": "status",
                "value": "1"
                }
            ]
        */
        [HttpPatch("{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> PartialDeviceStatusUpdate(string deviceId, [FromBody]JsonPatchDocument<DeviceStaUpdateDto> patchDs)
        {
            var deviceStatusItem = await _repository.GetDeviceStatusByIdAsync(deviceId);
            if (deviceStatusItem == null)
            {
                return NotFound("NotFound");
            }

            var deviceStaUpdateDto = _mapper.Map<DeviceStaUpdateDto>(deviceStatusItem);

            patchDs.ApplyTo(deviceStaUpdateDto, ModelState);

            if (!TryValidateModel(deviceStaUpdateDto))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(deviceStaUpdateDto, deviceStatusItem);

            await _repository.UpdateDeviceStatusAsync(deviceStatusItem);

            return Ok("Ok");
        }


        //DELETE api/deviceStatus/{deviceId}
        [HttpDelete("{deviceId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> DeleteDeviceStatus(string deviceId)
        {
            var deviceStatusItem = await _repository.GetDeviceStatusByIdAsync(deviceId);

            if (deviceStatusItem == null)
            {
                return NotFound("NotFound");
            }

            _repository.DeleteDeviceStatus(deviceStatusItem);
            await _repository.SaveAsync();

            return Ok("ok");
        }
    }
}