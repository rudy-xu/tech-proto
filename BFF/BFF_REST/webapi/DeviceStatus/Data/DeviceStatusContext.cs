using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class DeviceStatusContext: DbContext
    {
        public DeviceStatusContext(DbContextOptions<DeviceStatusContext> opt): base(opt)
        {
            //..
        }

        public DbSet<DeviceStatus> DeviceStatuses { set; get; }
    }
}