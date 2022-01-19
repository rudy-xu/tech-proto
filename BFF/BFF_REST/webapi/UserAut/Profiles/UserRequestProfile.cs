using AutoMapper;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class UserRequestProfile: Profile
    {
        public UserRequestProfile()
        {
            CreateMap<UserInfo, UserReadDto>();
            CreateMap<AuthenticateResponse, AuthenticateResponeDto>();

            CreateMap<UserLoginDto, UserRequest>();
            CreateMap<UserCreateDto, UserRequest>();
            CreateMap<UserUpdateDto, UserRequest>();
        }
    }
}