using WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IDeviceMsgInfoRepo
    {
        Task<IEnumerable<string>> GetAgentListAsync();

        Task<IEnumerable<string>> GetDeviceListAsync();

        Task<IEnumerable<string>> GetDeviceListByAgentAsync(string agentId);

        Task<IEnumerable<AllDeviceMsg>> GetAllDeviceMsgInfosAsync();
        
        Task<IEnumerable<DeviceMsgInfo>> GetDeviceMsgInfoByIdAsync(string deviceId);

        Task<DeviceMsgInfo> GetNewestMsgInfoAsync(string deviceId);

        Task<DeviceMsgInfo> GetDeviceMsgInfoByRecordAsync(string record);

        Task<DeviceMsgInfo> GetDeviceMsgInfoByTimeAsync(string ts);

        // Task AddDeviceMsgInfoAsync(DeviceMsgInfo dmi);
        
        Task UpdateDeviceMsgInfoAsync(DeviceMsgInfo dmi);

        void DeleteDeviceMsgInfo(DeviceMsgInfo dmi);

        Task<bool> SaveChangesAsync();
    }
}