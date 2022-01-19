using System.Collections.Generic;
using System.Threading.Tasks;
using webApi.Models;

namespace webApi.Data
{
    public interface ICourseRepo
    {
        Task<IEnumerable<Teacher>> GetAllTeachersInfoAsync();

        Task<Teacher> GetTeacherByIdAsync(string tchId);

        Task<Teacher> GetTeacherByTimestampAsync(string ts);

        Task<IEnumerable<Teacher>> GetSelectedTeacherAsync();

        Task<IEnumerable<AllCourseInfo>> GetAllCourseInfoAsync();

        Task<Course> GetCourseByIdAsync(int crsId);

        //Orders by timestamp
        Task<Course> GetTeacherTopCourseInfoAsync(string tchId);

        Task<IEnumerable<StuCourse>> GetStuCourseByIdAsync(string stuId);

        Task AddCourseInfoAsync(SelCourseInfo crs);

        Task UpdateCourseInfoAsync(Course crs);

        Task DeleteCourseInfo(string crsId);

        Task<bool> SaveAsync();
    }
}