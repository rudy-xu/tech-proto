using System.Threading.Tasks;
using System.Collections.Generic;
using webApi.Models;

namespace webApi.Data
{
    public interface IUserInfoRepo
    {
        Task<AuthResponse> GetAuthInfoAsync(UserRequest user);

        Task<IEnumerable<UserInfo>> GetAllUserInfosAsync();

        Task<UserInfo> GetUserInfoByIdAsync(int id);

        Task AddUserInfoAsync(UserRequest user);

        //0:password is same
        //1:password modifies successfully
        Task<int> UpdateUserPwdInfoAsync(UserRequest user);

        Task DeleteUserInfoAsync(int id);

        Task<bool> SaveAsync();
    }
}
