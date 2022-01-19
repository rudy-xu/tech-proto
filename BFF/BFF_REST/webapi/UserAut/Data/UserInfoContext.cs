using WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Data
{
    public class UserInfoContext: DbContext
    {
        public UserInfoContext(DbContextOptions<UserInfoContext> opt): base(opt)
        {
            //...
        }

        public DbSet<UserInfo> UserInfos { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserInfo>(entity => {
                entity.HasKey(k => k.ID);
            });
        }
    }
}