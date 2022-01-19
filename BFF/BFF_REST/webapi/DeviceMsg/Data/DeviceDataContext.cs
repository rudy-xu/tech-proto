using WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data
{
    public class DeviceDataContext: DbContext
    {
        public DeviceDataContext(DbContextOptions<DeviceDataContext> opt): base(opt)
        {

        }

        public DbSet<DeviceInfo> DeviceInfos { set; get; }
        public DbSet<DeviceSessInfo> DeviceSessInfos { set; get; }
        public DbSet<DeviceMsgInfo> DeviceMsgInfos { set; get; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceInfo>(entity => {
                entity.HasKey(k => k.DeviceID);
            });

            modelBuilder.Entity<DeviceSessInfo>(entity => {
                entity.HasKey(k => k.SessionID);
            });

            modelBuilder.Entity<DeviceMsgInfo>(entity => {
                entity.HasKey(k => k.RecordID);
            });
        }
    }
}
