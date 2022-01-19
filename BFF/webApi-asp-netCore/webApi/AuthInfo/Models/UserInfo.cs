using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    [Table("tb_user")]
    public class UserInfo
    {
        [Key]
        [Required]
        [Column("id")]
        public int ID { get; set; }    //Auot Increment

        [Column("userName")]
        public string UserName { get; set; }

        //Change pwd to pwdHash and pwdSalt
        // [Column("pwd")]
        // public string Pwd { get; set; }

        [Column("pwdHash")]
        public byte[] PwdHash { get; set; }

        [Column("pwdSalt")]
        public byte[] PwdSalt { get; set; }
    }
}
