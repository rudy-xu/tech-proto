using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class DeviceMsgInfoUpdate
    {
        [Required]
        public string RecordID { set; get; }

        public string SessionID { set; get; }

        public string TimeStamp { set; get; }

        [Required]
        public string Data { set; get; }
    }
}