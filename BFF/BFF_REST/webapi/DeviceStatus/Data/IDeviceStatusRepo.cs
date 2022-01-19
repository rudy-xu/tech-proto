using System.Threading.Tasks;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Data
{
    public interface IDeviceStatusRepo
    {
        Task<IEnumerable<DeviceStatus>> GetAllDeviceStatusesAsync();

        Task<DeviceStatus> GetDeviceStatusByIdAsync(string deviceId);

        Task<DeviceStatus> GetDeviceStatusByTimeAsync(string ts);

        Task AddDeviceStatusAsync(DeviceStatus ds);

        Task UpdateDeviceStatusAsync(DeviceStatus ds);

        void DeleteDeviceStatus(DeviceStatus ds);

        Task<bool> SaveAsync();
    }
}