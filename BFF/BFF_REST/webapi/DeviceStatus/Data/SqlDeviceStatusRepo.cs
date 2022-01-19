using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public class SqlDeviceStatusRepo: IDeviceStatusRepo
    {
        private readonly DeviceStatusContext _context;

        public SqlDeviceStatusRepo(DeviceStatusContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeviceStatus>> GetAllDeviceStatusesAsync()
        {
            return await _context.DeviceStatuses.ToListAsync();
        }

        public async Task<DeviceStatus> GetDeviceStatusByIdAsync(string deviceId)
        {
            return await _context.DeviceStatuses.FindAsync(deviceId);   //FirstOrDefaultAsync(p => p.DeviceID == deviceId);
        }

        public async Task<DeviceStatus> GetDeviceStatusByTimeAsync(string ts)
        {
            return await _context.DeviceStatuses.FirstOrDefaultAsync(p => p.TimeStamp == ts);
        }

        public async Task AddDeviceStatusAsync(DeviceStatus ds)
        {
            if(ds == null)
            {
                throw new ArgumentException(nameof(ds));
            }

           await _context.DeviceStatuses.AddAsync(ds);
           await _context.SaveChangesAsync();
        }
        
        public async Task UpdateDeviceStatusAsync(DeviceStatus ds)
        {
            if(ds == null)
            {
                throw new ArgumentException(nameof(ds));
            }
            
            _context.Entry(ds).State = EntityState.Modified;
            
            // var deviceStatusItem = _context.DeviceStatuses.Find(deviceId);

            // if(deviceStatusItem == null)
            // {
            //     throw new ArgumentException(nameof(deviceStatusItem));
            // }

            // deviceStatusItem.Status = ds.Status;
            // deviceStatusItem.TimeStamp = ds.TimeStamp;

            await _context.SaveChangesAsync();
        }

        public void DeleteDeviceStatus(DeviceStatus ds)
        {
            if(ds == null)
            {
                throw new ArgumentException(nameof(ds));
            }

            _context.DeviceStatuses.Remove(ds);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}