using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("tb_msg_info")]
    public class DeviceMsgInfo
    {
        [Key]
        [Required]
        [Column("record_uuid")]
        public string RecordID { set; get; }

        [Column("session_uuid")]
        public string SessionID { set; get; }

        [Column("timeStamp")]
        public string TimeStamp { set; get; }

        [Column("data")]
        public string Data { set; get; }
    }
}