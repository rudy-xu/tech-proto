using System;
using System.Linq;   //Enumerable, Queryable, DbSet<T> implement IQueryable<T> and IEnumerable<T>
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApi.Models;


namespace webApi.Data
{
    public class SqlCourseRepo : ICourseRepo
    {
        private readonly SelectCrsContext _context;

        public SqlCourseRepo(SelectCrsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersInfoAsync()
        {
            IQueryable<Teacher> teachersInfoQuery = from tch in _context.Teachers
                                                    select tch;

            return await teachersInfoQuery.AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(string tchId)
        {
            return await _context.Teachers.FindAsync(tchId);
        }

        public async Task<Teacher> GetTeacherByTimestampAsync(string ts)
        {
            return await _context.Teachers.FirstOrDefaultAsync(p => p.TimeStamp == ts);
        }

        public async Task<IEnumerable<Teacher>> GetSelectedTeacherAsync()
        {
            IQueryable<Teacher> teacherQuery = from tch in _context.Teachers
                                               where (from stu in _context.Students select stu.Tch_ID).Distinct().Contains(tch.Tch_ID)
                                               select tch;

            return await teacherQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AllCourseInfo>> GetAllCourseInfoAsync()
        {
            IQueryable<AllCourseInfo> allCourseInfoQuery = from tch in _context.Teachers
                                                           from stu in _context.Students
                                                           from crs in _context.Courses
                                                           where tch.Tch_ID == stu.Tch_ID && stu.Stu_ID == crs.Stu_ID
                                                           select new AllCourseInfo
                                                           {
                                                               TchName = tch.Name,
                                                               StuName = stu.Name,
                                                               CrsName = crs.CrsName,
                                                               TimeStamp = crs.Timestamp,
                                                               Remark = crs.Remark
                                                           };
            return await allCourseInfoQuery.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int crsId)
        {
            return await _context.Courses.FindAsync(crsId);
        }

        public async Task<Course> GetTeacherTopCourseInfoAsync(string tchId)
        {
            var studentItem = await _context.Students.FirstOrDefaultAsync(p => p.Tch_ID == tchId);

            if(studentItem == null)
            {
                return null;
            }

            IQueryable<Course> courseInfoQuery = from stu in _context.Students
                                                from crs in _context.Courses
                                                where stu.Stu_ID == crs.Stu_ID && stu.Tch_ID == tchId
                                                orderby crs.Timestamp descending //ascending
                                                select crs;

            // IQueryable<Course> courseInfoQuery =(from stu in _context.Students
            //                                      from crs in _context.Courses
            //                                      where stu.Stu_ID == crs.Stu_ID && stu.Tch_ID == tchId
            //                                      orderby crs.Timestamp descending
            //                                      select crs).Take(1);

            return await courseInfoQuery.AsNoTracking().FirstAsync();
        }

        public async Task<IEnumerable<StuCourse>> GetStuCourseByIdAsync(string stuId)
        {
            IQueryable<StuCourse> stuCourseQuery = from stu in _context.Students
                                                   from crs in _context.Courses
                                                   where stu.Stu_ID == crs.Stu_ID && stu.Stu_ID == stuId
                                                   select new StuCourse
                                                   {
                                                       StuId = stu.Stu_ID,
                                                       StuName = stu.Name,
                                                       CrsName = crs.CrsName,
                                                       TimeStamp = crs.Timestamp
                                                   };

            return await stuCourseQuery.AsNoTracking().ToListAsync();
        }

        //Cause having relationship in three tbales
        //Saves immediately when update someone table
        public async Task AddCourseInfoAsync(SelCourseInfo sci)
        {
            //uodate tb_teacher
            await _context.Teachers.AddAsync(new Teacher()
            {
                Tch_ID = sci.TchId,
                Name = sci.TchName,
                Major = sci.TchMajor,
                TimeStamp = sci.TchTs
            });
            await _context.SaveChangesAsync();

            //update tb_student
            await _context.Students.AddAsync(new Student()
            {
                Stu_ID = sci.StuId,
                Name = sci.StuName,
                Tch_ID = sci.TchId
            });
            await _context.SaveChangesAsync();

            //update tb_course
            await _context.Courses.AddAsync(new Course()
            {
                // Crs_Seq = sci.CrsSeq,
                Stu_ID = sci.StuId,
                CrsName = sci.CrsName,
                Timestamp = sci.CrsTs,
                Remark = sci.Remark

            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourseInfoAsync(Course inputCrs)
        {
            _context.Entry(inputCrs).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // //Manual modification
            // var tempCrs = await _context.Courses.FindAsync(crsId);
            // if(tempCrs ==null)
            // {
            //     throw new ArgumentException(nameof(tempCrs));
            // }

            // tempCrs.CrsName = crs.CrsName;
            // tempCrs.Timestamp = crs.Timestamp;

            // await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseInfo(string crsId)
        {
            var courseItem = await _context.Courses.FindAsync(Int32.Parse(crsId));
            if (courseItem == null)
            {
                throw new ArgumentException($"{crsId} not empty");
            }

            _context.Courses.Remove(courseItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}