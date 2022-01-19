using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class UserPartialUpdateDto
    {
        [Required]
        public string User { get; set; }

        public byte[] PwdHash { get; set; }

        public byte[] PwdSalt { get; set; }
    }
}
