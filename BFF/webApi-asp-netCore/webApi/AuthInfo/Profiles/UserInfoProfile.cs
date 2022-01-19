using webApi.Models;
using webApi.Dtos;
using AutoMapper;

namespace webApi.Profiles
{
    public class UserInfoProfile: Profile
    {
        public UserInfoProfile()
        {
            //Model -> Dto
            CreateMap<UserInfo, UserInfoReadDto>();
            CreateMap<UserInfo, UserPartialUpdateDto>();
            CreateMap<AuthResponse, AuthResponseDto>();

            //Dto -> model
            CreateMap<UserRequestDto, UserRequest>();
            CreateMap<UserPartialUpdateDto, UserInfo>();

        }
    }
}
