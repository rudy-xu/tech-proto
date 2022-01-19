using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class DeviceStaCreateDto
    {
        [Required]
        public string DeviceID { set; get; }

        [Required]
        public int Status { set; get; }

        public string TimeStamp { set; get; }
    }
}