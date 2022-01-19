using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    [Table("tb_student")]
    public class Student
    {
        [Key]
        [Required]
        [Column("stu_uuid")]
        public string Stu_ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tch_uuid")]
        public string Tch_ID { get; set;}
    }
}