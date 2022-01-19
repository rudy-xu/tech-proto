using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Models
{
    [Table("tb_user_info")]
    public class UserInfo
    {
        [Key]
        [Column("id")]
        public int ID { set; get; }     //Auto increment

        [Required]
        [Column("userName")]
        public string UserName { set; get; }

        // // [Required]
        // [Column("password")]
        // [JsonIgnore]   //???
        // public string Password { set; get; }

        [Required]
        [Column("pwdHash")]
        public byte[] PwdHash { set; get; }

        [Required]
        [Column("pwdSalt")]
        public byte[] PwdSalt { set; get; }
    }
}