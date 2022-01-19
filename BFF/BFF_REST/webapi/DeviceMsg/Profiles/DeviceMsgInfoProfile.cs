using AutoMapper;
using WebApi.Models;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class DeviceMsgInfoProfile: Profile
    {
        public DeviceMsgInfoProfile()
        {
            CreateMap<AllDeviceMsg, AllDeviceMsgReadDto>();
            CreateMap<DeviceMsgInfo, DeviceMsgInfoReadDto>();

            CreateMap<DeviceMsgInfoUpdate, DeviceMsgInfo>();
        }
    }
}