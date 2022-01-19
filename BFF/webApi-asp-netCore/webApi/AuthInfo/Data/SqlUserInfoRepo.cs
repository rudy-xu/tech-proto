using System.Text;   //Encoding.ASCII
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;           //ClaimsIdentity
using System.IdentityModel.Tokens.Jwt;   //JwtSecurityTokenHandler
using Microsoft.IdentityModel.Tokens; //SecurityTokenDescriptor, SigningCredentials
using Microsoft.Extensions.Options;  //IOptions<T> -> Used to retrieve configured IOptions instances.
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Data
{
    public class SqlUserInforepo : IUserInfoRepo
    {
        private readonly UserInfoContext _context;

        private readonly AppSettings _appSettings;
        public SqlUserInforepo(UserInfoContext context, IOptions<AppSettings> appSettings)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _appSettings = appSettings.Value;
        }

        public async Task<AuthResponse> GetAuthInfoAsync(UserRequest userRequest)
        {
            if(string.IsNullOrEmpty(userRequest.UserName) || string.IsNullOrEmpty(userRequest.Password))
            {
                return null;
            }

            //Search the user in tb_user
            var userItem = await _context.UserInfos.FirstOrDefaultAsync(p => p.UserName == userRequest.UserName);

            //Check password
            // if(!((userItem.Pwd).Equals(userRequest.Password)))
            // {
            //     return null;
            // }

            //Check pwdHash and pwdSalt
            if(!VerifyPwd(userRequest.Password, userItem.PwdSalt, userItem.PwdHash))
            {
                //Password is incorrect
                return null;
            }

            //Generate token by custom method
            string token = GenerateToken(userItem);

            return new AuthResponse(userItem, token);
        }

        //Generate and Set the token valid time to 7 days
        private string GenerateToken(UserInfo userInfo)
        {
            /*
                SecurityTokenDescriptor：
                    This is a place holder for all the attributes related to the issued token.
                ClaimsIdentity：
                    Claim represents a claim unit, which is used to form ClaimsIdentity. 
                    ClaimsIdentity represents a credentials, such as an ID card. 
                    The name on the ID card represents a claim, and the ID number also represents a claim. 
                    All of these claims form an ID card, which is ClaimsIdentity.
                Claim:
                Fucntion: public Claim (string type, string value);
                    a new instance of the Claim class with the specified claim type, and value
                    
                SigningCredentials:
                Fucntion: SigningCredentials(SecurityKey key, string algorithm)
                    Gets or sets the credentials that are used to sign the token.
                   
                SymmetricSecurityKey(byte[] key):
                    Represents the abstract base class for all keys that are generated using symmetric algorithms.
            */

            //encodes a set of characters into a sequence of bytes.
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor{
                //Subject: Gets or sets the output claims to be included in the issued token.
                Subject = new ClaimsIdentity(new[] { new Claim("id", userInfo.ID.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(5),
                // Expires = DateTime.UtcNow.AddHours(5),
                // Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //A SecurityTokenHandler designed for creating and validating Json Web Tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);  //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format(xx.xx.xx).
        }

        public async Task<IEnumerable<UserInfo>> GetAllUserInfosAsync()
        {
            return await _context.UserInfos.ToListAsync();
        }

        public async Task<UserInfo> GetUserInfoByIdAsync(int id)
        {
            return await _context.UserInfos.FindAsync(id);
        }

        public async Task AddUserInfoAsync(UserRequest userRequest)
        {
            //Generate salt and hash in password
            byte[] pwdSalt, pwdHash;
            GeneratePwdSaltHash(userRequest.Password, out pwdSalt, out pwdHash);

            UserInfo userInfo = new UserInfo{
                UserName = userRequest.UserName,
                PwdHash = pwdHash,
                PwdSalt = pwdSalt
            };
            await _context.UserInfos.AddAsync(userInfo);
            await _context.SaveChangesAsync();
        }

        //Generate salt and hash in password
        private void GeneratePwdSaltHash(string pwd, out byte[] pwdSalt, out byte[] pwdHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pwdSalt = hmac.Key;     //HMACSHA512() uses a randomly generated key; So Use as Salt
                pwdHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd));   //HMACSHA512() generate 64bytes -> 512bit, Then calculate hash value
            }
        }
        
        private bool VerifyPwd(string pwd, byte[] pwdSalt, byte[] pwdHash)
        {
            //Uses same key(pwdSalt) generate HMACSHA512 class
            using(var hmac = new System.Security.Cryptography.HMACSHA512(pwdSalt))
            {
                //Uses same HMACSHA512 class to calculate hash and compare
                var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                for(int i=0; i< ComputeHash.Length; ++i)
                {
                    if(ComputeHash[i] != pwdHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<int> UpdateUserPwdInfoAsync(UserRequest userRequest)
        {
            //Cause to Generate Salt and hash
            //Can not use the following method
            // _context.Entry(user).State = EntityState.Modified;

            var userItem = await _context.UserInfos.FirstOrDefaultAsync(p => p.UserName == userRequest.UserName);
            if(VerifyPwd(userRequest.Password, userItem.PwdSalt, userItem.PwdHash))
            {
                return 0;
            }
            else
            {
                byte[] pwdSalt, pwdHash;
                this.GeneratePwdSaltHash(userRequest.Password, out pwdSalt, out pwdHash);

                userItem.PwdSalt = pwdSalt;
                userItem.PwdHash = pwdHash;
                await _context.SaveChangesAsync();

                return 1;
            }
        }

        public async Task DeleteUserInfoAsync(int id)
        {
            var userItem = await _context.UserInfos.FindAsync(id);
            _context.UserInfos.Remove(userItem);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}
