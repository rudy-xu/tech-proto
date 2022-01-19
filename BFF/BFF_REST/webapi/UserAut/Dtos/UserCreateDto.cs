using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string Password { set; get; }
    }
}