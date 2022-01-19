using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserRequest
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string Password { set; get; }
    }
}