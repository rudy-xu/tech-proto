using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string Password { set; get; }
    }
}