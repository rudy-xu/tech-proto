using WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IDeviceInfoRepo
    {
        //a set of data
        Task<IEnumerable<DeviceInfo>> GetAllDeviceInfosAsync();

        Task<IEnumerable<string>> GetDeviceLsitAsync();

        Task<IEnumerable<string>> GetAgentListAsync();

        //a piece of data
        Task<DeviceInfo> GetDeviceInfoByIdAsync(string deviceId);

        // Task AddDeviceInfoAsync(DeviceInfo di);

        // Task UpadteDeviceInfoAsync(DeviceInfo di);

        // void DeleteDeviceInfo(DeviceInfo di);

        // Task<bool> SaveAsync();
    }
}