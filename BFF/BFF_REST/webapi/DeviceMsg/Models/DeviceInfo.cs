using System.ComponentModel.DataAnnotations.Schema;    //[Table],[Column]
using System.ComponentModel.DataAnnotations;  //[Key],[Required]

namespace WebApi.Models
{
    [Table("tb_device_info")]
    public class DeviceInfo
    {
        [Key]
        [Required]
        [Column("device_uuid")]
        public string DeviceID { set; get; }

        [Required]
        [Column("agent_uuid")]
        public string AgentID { set; get; }

        [Column("timeStamp")]
        public string TimeStamp { set; get; }
    }
}