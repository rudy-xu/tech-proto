using System.ComponentModel.DataAnnotations;

namespace webApi.Models
{
    public class UserRequestDto
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
