using Microsoft.EntityFrameworkCore;
using webApi.Models;

namespace webApi.Data
{
    public class UserInfoContext: DbContext
    {
        public UserInfoContext(DbContextOptions<UserInfoContext> opt): base(opt)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
