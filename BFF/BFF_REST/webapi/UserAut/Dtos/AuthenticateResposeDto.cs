using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class AuthenticateResponeDto
    {
        // public int Id { set; get; }
        public string userName { set; get; }

        [Required]
        public string Token { set; get; }
    }
}