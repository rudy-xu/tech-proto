using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class CourseUpdateDto
    {
        [Required]
        public int Crs_Seq { get; set; }

        public string Stu_ID { get; set; }

        [Required]
        public string CrsName { get; set; }

        public string Remark { get; set; }

        public string Timestamp { get; set; }
    }
}