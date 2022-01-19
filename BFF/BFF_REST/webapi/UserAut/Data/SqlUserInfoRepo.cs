using System.Security.Claims; //ClaimsIdentity
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;   //JwtSecurityTokenHandler
using Microsoft.IdentityModel.Tokens; //SecurityTokenDescriptor, SigningCredentials
using Microsoft.Extensions.Options;

namespace WebApi.Data
{
    public class SqlUserInfoRepo: IUserInfoRepo
    {
        private readonly UserInfoContext _context;
        private readonly AppSettings _appSettings;

        public SqlUserInfoRepo(IOptions<AppSettings> appSettings, UserInfoContext context)
        {
            _appSettings = appSettings.Value;
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        // public async Task<AuthenticateResponse> AuthenticateAsync(UserInfo userInfo)
        // {
        //     if(string.IsNullOrEmpty(userInfo.UserName) || string.IsNullOrEmpty(userInfo.Password))
        //     {
        //         return null;
        //     }

        //     var userItem = await _context.UserInfos.SingleOrDefaultAsync(p => p.UserName == userInfo.UserName && p.Password == userInfo.Password);

        //     if(userItem == null)
        //     {
        //         return null;
        //     }

        //     //custom method to generate token
        //     var token = GenerateJwtToken(userItem);

        //     return new AuthenticateResponse(userItem, token);
        // }
        
        public async Task<AuthenticateResponse> AuthenticateAsync(UserRequest userRequest)
        {
            if(string.IsNullOrEmpty(userRequest.UserName) || string.IsNullOrEmpty(userRequest.Password))
            {
                return null;
            }

            var userItem = await _context.UserInfos.SingleOrDefaultAsync(p => p.UserName == userRequest.UserName);
            // if(userItem.PwdHash.Length != 64)
            // {
            //     throw new ArgumentException("Invalid password hash");
            // }

            if(!VerifyPwd(userRequest.Password, userItem.PwdHash, userItem.PwdSalt))
            {
                return null;
            }

            //custom method to generate token
            var token = GenerateJwtToken(userItem);

            return new AuthenticateResponse(userItem, token);
        }
        
        public bool VerifyPwd(string pwd, byte[] pwdHash, byte[] pwdSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(pwdSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));  //Use pwd salt to create hash
                for(int i=0; i< computedHash.Length; ++i){
                    if(computedHash[i] != pwdHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private string GenerateJwtToken(UserInfo userItem)
        {
            //Set the token valid time to 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new[] {new Claim("id", userItem.ID.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                // Expires = DateTime.UtcNow.AddHours(5),
                // Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            return await _context.UserInfos.ToListAsync();
        }

        public async Task<UserInfo> GetUserByIdAsync(string userId)
        {
            return await _context.UserInfos.FindAsync(Int32.Parse(userId));
        }

        //There is no salt or hash in password.
        // public async Task AddUserAsync(UserInfo user)
        // {
        //     await _context.UserInfos.AddAsync(user);
        //     await _context.SaveChangesAsync();
        // }

        //salt and hash password
        public async Task CreateUserAsync(UserRequest userRequest)
        {
            byte[] pwdSalt, pwdHash;

            CreatePasswordHash(userRequest.Password, out pwdHash, out pwdSalt);

            UserInfo user = new UserInfo{
                UserName = userRequest.UserName,
                PwdHash = pwdHash,
                PwdSalt = pwdSalt
            };

            await _context.UserInfos.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public void CreatePasswordHash(string pwd, out byte[] pwdHash, out byte[] pwdSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pwdSalt = hmac.Key;
                pwdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));
            }
        } 

        public async Task UpdateUserAsync(UserRequest userRequest)
        {
            var userItem = await _context.UserInfos.SingleOrDefaultAsync(p => p.UserName == userRequest.UserName);
            if(VerifyPwd(userRequest.Password, userItem.PwdHash, userItem.PwdSalt))
            {
                byte[] pwdSalt, pwdHash;
                CreatePasswordHash(userRequest.Password, out pwdHash, out pwdSalt);

                userItem.PwdHash = pwdHash;
                userItem.PwdSalt = pwdSalt;
            };

            // _context.Entry(user).State = EntityState.Modified;
            // _context.UserInfos.Update(userItem);
            await _context.SaveChangesAsync();
        }

        public void DeleteUser(UserInfo user)
        {
            _context.UserInfos.Remove(user);
            _context.SaveChangesAsync();
        }       

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
