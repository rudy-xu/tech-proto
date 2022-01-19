using AutoMapper;
using webApi.Models;
using webApi.Dtos;

namespace webApi.Profiles
{
    public class SelCourseProfile: Profile
    {
        public SelCourseProfile()
        {
            //Map Teacher -> TeacherReadDto
            CreateMap<Teacher, TeacherReadDto>();
            CreateMap<Course, CourseReadDto>();
            CreateMap<Course, CourseUpdateDto>();

            //Map SelCourseCreateDto -> SelCourseInfo
            CreateMap<SelCourseCreateDto, SelCourseInfo>();
            CreateMap<CourseUpdateDto, Course>();
        }
    }
}