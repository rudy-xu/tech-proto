using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class SelCourseCreateDto
    {
        [Required]
        public string TchId { get; set; }

        public string TchName { get; set; }

        public string TchMajor { get; set; }

        public string TchTs { get; set; }

        [Required]
        public string StuId { get; set; }

        public string StuName { get; set; }

        [Required]
        public string CrsName { get; set; }

        public string CrsTs { get; set; }

        public string Remark { get; set; }
    }
}