namespace webApi.Models
{
    public class AuthResponse
    {
        public int ID { get; set; }

        public string User { get; set; }

        public string Token { get; set; }

        public AuthResponse(UserInfo userInfo, string token)
        {
            ID = userInfo.ID;
            User = userInfo.UserName;
            Token = token;
        }
    }
}
