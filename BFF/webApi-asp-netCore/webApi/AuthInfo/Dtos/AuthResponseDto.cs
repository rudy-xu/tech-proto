using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class AuthResponseDto
    {
        public string User { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
