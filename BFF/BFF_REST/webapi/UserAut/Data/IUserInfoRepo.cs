using System.Threading.Tasks;
using WebApi.Models;
using System.Collections.Generic;

namespace WebApi.Data
{
    public interface IUserInfoRepo
    {
        //Generate token
        Task<AuthenticateResponse> AuthenticateAsync(UserRequest userRequest);
        
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();

        Task<UserInfo> GetUserByIdAsync(string userId);

        Task CreateUserAsync(UserRequest userRequest);

        Task UpdateUserAsync(UserRequest userRequest);

        void DeleteUser(UserInfo user);

        Task<bool> SaveChangesAsync();

    }
}