using System.Runtime.Serialization;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public int Id { set; get; }

        public string userName { set; get; }
        public string Token { set; get; }

        public AuthenticateResponse(UserInfo userInfo, string token)
        {
            Id = userInfo.ID;
            userName = userInfo.UserName;
            Token = token;
        }
    }
}