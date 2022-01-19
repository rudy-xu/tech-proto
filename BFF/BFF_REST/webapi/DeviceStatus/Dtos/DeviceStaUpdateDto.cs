using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class DeviceStaUpdateDto
    {
        [Required]
        public string DeviceID { set; get; }

        public int Status { set; get; }

        public string TimeStamp { set; get; }
    }
}