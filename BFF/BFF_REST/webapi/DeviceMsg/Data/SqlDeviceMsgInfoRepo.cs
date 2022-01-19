using WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class SqlDeviceMsgInfoRepo : IDeviceMsgInfoRepo
    {
        private readonly DeviceDataContext _context;

        public SqlDeviceMsgInfoRepo(DeviceDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAgentListAsync()
        {
            IQueryable<string> agentQuery = (from device in _context.DeviceInfos
                                             select device.AgentID).Distinct();

            return await agentQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDeviceListAsync()
        {
            IQueryable<string> devicesQuery = from device in _context.DeviceInfos
                                              select device.DeviceID;

            return await devicesQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<string>> GetDeviceListByAgentAsync(string agentId)
        {
            IQueryable<string> deviceQuery = from device in _context.DeviceInfos
                                             where device.AgentID == agentId
                                             select device.DeviceID;

            return await deviceQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AllDeviceMsg>> GetAllDeviceMsgInfosAsync()
        {
            //Query syntax
            IQueryable<AllDeviceMsg> msgQuery = from session in _context.DeviceSessInfos
                                                from msg in _context.DeviceMsgInfos
                                                where session.SessionID == msg.SessionID
                                                select new AllDeviceMsg
                                                {
                                                    deviceId = session.DeviceID,
                                                    recordId = msg.RecordID,
                                                    timestamp = msg.TimeStamp,
                                                    data = msg.Data
                                                };

            // return await _context.DeviceMsgInfos.ToListAsync();
            return await msgQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeviceMsgInfo>> GetDeviceMsgInfoByIdAsync(string deviceId)
        {
            //Method syntax
            // IQueryable<DeviceMsg> msgIQ = from session in _context.DeviceSessions
            //                 from msg in _context.DeviceMsgs.Where(msg => session.DeviceSessUuid == msg.DeviceSessUuid).DefaultIfEmpty()
            //                 select msg;

            //Query syntax
            IQueryable<DeviceMsgInfo> msgQuery = from session in _context.DeviceSessInfos
                                                 from msg in _context.DeviceMsgInfos
                                                 where session.SessionID == msg.SessionID && session.DeviceID == deviceId
                                                 select msg;

            // IQueryable<DeviceMsgInfo> msgQuery = from deviceInfo in _context.DeviceInfos
            //                                      from session in _context.DeviceSessInfos
            //                                      from msg in _context.DeviceMsgInfos
            //                                      where deviceInfo.DeviceID == session.DeviceID && session.SessionID == msg.SessionID && deviceInfo.DeviceID == deviceId
            //                                      select msg;

            return await msgQuery.AsNoTracking().ToListAsync();
        }

        public async Task<DeviceMsgInfo> GetNewestMsgInfoAsync(string deviceId)
        {
            IQueryable<DeviceMsgInfo> msgQuery = from session in _context.DeviceSessInfos
                                                 from msg in _context.DeviceMsgInfos
                                                 where session.SessionID == msg.SessionID && session.DeviceID == deviceId
                                                 orderby msg.TimeStamp descending
                                                 select msg;

            return await msgQuery.AsNoTracking().FirstAsync();
        }

        public async Task<DeviceMsgInfo> GetDeviceMsgInfoByRecordAsync(string record)
        {
            return await _context.DeviceMsgInfos.FindAsync(record);
        }
        public async Task<DeviceMsgInfo> GetDeviceMsgInfoByTimeAsync(string ts)
        {
            return await _context.DeviceMsgInfos.FirstOrDefaultAsync(time => time.TimeStamp == ts);
        }

        public void DeleteDeviceMsgInfo(DeviceMsgInfo dmi)
        {
            if(dmi == null)
            {
                throw new ArgumentException(nameof(dmi));
            }

            _context.DeviceMsgInfos.Remove(dmi);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task UpdateDeviceMsgInfoAsync(DeviceMsgInfo dmi)
        {
            _context.Entry(dmi).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}