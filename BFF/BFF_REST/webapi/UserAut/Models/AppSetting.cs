/*
    The "Secret" property is used by the api to sign and verify JWT tokens for authentication, 
    update it with your own random string to ensure nobody else can generate a JWT to gain unauthorised access to your application.

    This property content is write in the appSettings.json file.
*/

namespace WebApi.Models
{
    public class AppSettings
    {
        public string Secret { set; get; }
    }
}