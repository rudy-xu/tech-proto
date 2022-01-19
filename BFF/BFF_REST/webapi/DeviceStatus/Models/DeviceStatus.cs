using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_status")]
    public class DeviceStatus
    {
        [Column("device_uuid")]
        [Key]
        public string DeviceID { set; get; }

        // [MaxLength(250)] -- limit length
        [Column("status")]
        [Required]            //not null
        public int Status { set; get; }

        [Column("timeStamp")]
        public string TimeStamp {set; get; }
    }
}