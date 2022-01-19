using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IDeviceSessInfoRepo
    {
        Task<IEnumerable<DeviceSessInfo>> GetAllDeviceSessInfosAsync();

        Task<DeviceSessInfo> GetDeviceSessInfoByIdAsync(string sessionId);

        // Task AddDeviceSessInfoAsync(DeviceInfo di);

        // Task UpadteDeviceSessInfoAsync(DeviceInfo di);

        // void DeleteDeviceSessInfo(DeviceInfo di);

        // Task<bool> SaveAsync();
    }
}