using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using webApi.Models;
using webApi.Data;
using webApi.Dtos;
using webApi.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Cors;

namespace webApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserInfoController: ControllerBase
    {
        private readonly IUserInfoRepo _repository;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //POST api/user/Auth
        //Get Authenication token
        [HttpPost("{Auth}")]
        public async Task<ActionResult<AuthResponseDto>> GetAuthInfoAsync(UserRequestDto userRequestDto)
        {
            if(userRequestDto == null)
            {
                return NotFound("Not Found");
            }

            var userRequest = _mapper.Map<UserRequest>(userRequestDto);

            var userRespone = await _repository.GetAuthInfoAsync(userRequest);

            if(userRespone == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<AuthResponseDto>(userRespone));
        }

        //GET api/user
        [HttpGet]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<UserInfoReadDto>>> GetAllUserInfosAsync()
        {
            var userInfoItems = await _repository.GetAllUserInfosAsync();

            if(userInfoItems == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<IEnumerable<UserInfoReadDto>>(userInfoItems));
        }

        //GET api/user/{userId}
        [HttpGet("{userId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<UserInfoReadDto>> GetUserInfoByIdAsync(string userId)
        {
            var userInfoItem = await _repository.GetUserInfoByIdAsync(Int32.Parse(userId));
            if(userInfoItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<UserInfoReadDto>(userInfoItem));
        }

        //POST api/user/add
        [HttpPost("add")]
        // [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> AddUserInfoAsync(UserRequestDto userRequestDto)
        {
            if(userRequestDto == null)
            {
                return NotFound("Input empty object");
            }

            var userRequest = _mapper.Map<UserRequest>(userRequestDto);

            await _repository.AddUserInfoAsync(userRequest);

            return Ok("Ok");
        }

        //PUT api/user/update
        [HttpPut("update")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> UpdateUserInfoAsync(UserRequestDto userRequestDto)
        {
            if(userRequestDto == null)
            {
                return NotFound("Input empty object");
            }

            var userRequest = _mapper.Map<UserRequest>(userRequestDto);
            int res = await _repository.UpdateUserPwdInfoAsync(userRequest);
            if(res == 0)
            {
                return Ok("Password is same");
            }
            else
            {
                 return Ok("Modify Successfully");
            }
        }

        /*
            For my opinion, this function is not suited for update hash and salt.
            "Patch" use "ApplyTo" to implement updating partial part.
            However, the hash and salt is not esay to get by users.
            It can use to update some information that users can provide.
        */
        //PATCH api/user/update/{userId}
        [HttpPatch("update/{userId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> PartialUpdateUserInfoAsync(string userId, [FromBody] JsonPatchDocument<UserPartialUpdateDto> userPartialUpdateDto)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound("ID empty");
            }

            var userInfoItem = await _repository.GetUserInfoByIdAsync(Int32.Parse(userId));
            if(userInfoItem == null)
            {
                return NotFound("Not Found");
            }
            var userItemUpdateDto = _mapper.Map<UserPartialUpdateDto>(userInfoItem);

            userPartialUpdateDto.ApplyTo(userItemUpdateDto, ModelState);

            _mapper.Map(userItemUpdateDto, userInfoItem);

            await _repository.SaveAsync();

            return Ok("Ok");
        }

        //DELETE api/user/{userId}
        [HttpDelete("{userId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> DeleteUserInfoAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound("Input empty Id");
            }

            await _repository.DeleteUserInfoAsync(Int32.Parse(userId));

            return Ok("Ok");
        }
    }
}
