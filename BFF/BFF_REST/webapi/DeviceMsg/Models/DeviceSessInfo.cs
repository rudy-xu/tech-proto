using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_session_info")]
    public class DeviceSessInfo
    {
        [Key]
        [Required]
        [Column("session_uuid")]
        public string SessionID{ set; get; }

        [Column("device_uuid")]
        public string DeviceID { set; get; }
    }
}