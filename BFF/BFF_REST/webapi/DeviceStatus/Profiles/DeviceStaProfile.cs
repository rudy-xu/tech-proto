using AutoMapper; //Profile
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Profiles
{
    //Profile is another way to organize mapping\
    //Configure the mapping in the construct
    //Based on the same field name mapping and is case insensitive
    //Advice: Use same naming rules
    public class DeviceStaProfile: Profile
    {
        public DeviceStaProfile()
        {
            //model(source) -> map object(Target object)
            CreateMap<DeviceStatus, DeviceStaReadDto>();

            CreateMap<DeviceStaCreateDto, DeviceStatus>();
            CreateMap<DeviceStaUpdateDto, DeviceStatus>();
        }
    }
}