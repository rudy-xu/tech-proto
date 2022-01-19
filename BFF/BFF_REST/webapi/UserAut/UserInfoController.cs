using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using WebApi.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using AutoMapper;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserInfoController: ControllerBase
    {
        private readonly IUserInfoRepo _repository;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //POST api/user/authenticate
        /*
            {
                "userName": "tony",
                "password": "1234536"
            }
        */
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponeDto>> AuthenticateAsync(UserLoginDto userRequestLoginDto)
        {
            var userRequest = _mapper.Map<UserRequest>(userRequestLoginDto);

            if(userRequest == null)
            {
                return BadRequest("Input Error.");
            }

            var response = await _repository.AuthenticateAsync(userRequest);

            if(response == null)
            {
                return BadRequest("UserName or password is incorrect");
            }

            return Ok(_mapper.Map<AuthenticateResponeDto>(response));
        }

        //GET api/user
        [HttpGet]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsersAsync()
        {
            var userItems = await _repository.GetAllUsersAsync();
            if(userItems == null)
            {
                return NotFound("NotFound");
            }

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems));
        }

        //GET api/user/{userId}
        [HttpGet("{userId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<UserReadDto>> GetUserById(string userId)
        {
            if(userId == null)
            {
                return NotFound("NotFound");
            }

            var userItem = await _repository.GetUserByIdAsync(userId);

            if(userItem == null)
            {
                return NotFound("NotFound");
            }

            return Ok(_mapper.Map<UserReadDto>(userItem));
        }

        //POST api/user/register
        [HttpPost("register")]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> RegisterAsync(UserCreateDto userCreateDto)
        {
            var userRequest = _mapper.Map<UserRequest>(userCreateDto);

            if(userRequest == null)
            {
                return BadRequest("Input Error");
            }

            await _repository.CreateUserAsync(userRequest);

            return Ok("Ok");
        }

        //PUT api/user/{userName}
        [HttpPut("{userName}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var userRequest = _mapper.Map<UserRequest>(userUpdateDto);

            if(userRequest == null)
            {
                return BadRequest("Input Error");
            }

            await _repository.UpdateUserAsync(userRequest);

            return Ok("Ok");
        }

        //DELETE api/user/{userId}
        [HttpDelete("{userId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            if(userId == null)
            {
                return BadRequest("ID Error");
            }

            var userItem = await _repository.GetUserByIdAsync(userId);
            if(userItem == null)
            {
                return NotFound("NotFound");
            }

            _repository.DeleteUser(userItem);

            return Ok("Ok");
        }
    }
}
