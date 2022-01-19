using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webApi.Models
{
    [Table("tb_course")]
    public class Course
    {
        [Key]
        [Required]
        [Column("crs_Seq")]
        public int Crs_Seq { get; set; }

        [Column("stu_uuid")]
        public string Stu_ID { get; set; }

        [Column("crsName")]
        public string CrsName { get; set; }

        [Column("remark")]
        public string Remark { get; set; }

        [Column("timeStamp")]
        public string Timestamp { get; set; }
    }
}