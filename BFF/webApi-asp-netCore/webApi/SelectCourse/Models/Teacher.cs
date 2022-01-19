using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    [Table("tb_teacher")]
    public class Teacher
    {
        [Key]
        [Required]
        [Column("tch_uuid")]
        public string Tch_ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("major")]
        public string Major { get; set; }

        [Column("timeStamp")]
        public string TimeStamp { get; set; }
    }
}