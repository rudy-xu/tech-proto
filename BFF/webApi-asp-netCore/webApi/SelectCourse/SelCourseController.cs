using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;   //HTTP(ApiController, Route,...)
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using AutoMapper;
using webApi.Dtos;
using webApi.Data;
using webApi.Models;
using webApi.Helper;
using Microsoft.AspNetCore.Cors;

namespace webApi.Controllers
{
    [ApiController]
    [Route("api/mis")]
    // [Route("api/[controller]")] -> Route("api/selCourse")
    public class SelCourseController: ControllerBase
    {
        /*
            Don't create a web API controller by deriving from the Controller class. 
            Controller derives from ControllerBase and adds support for views, 
            so it's for handling web pages, not web API requests. 
            There's an exception to this rule: if you plan to 
            use the same controller for both views and web APIs, derive it from Controller.

            The ControllerBase class provides many properties 
            and methods that are useful for handling HTTP requests. 
        */
        private readonly ICourseRepo _repository;
        private readonly IMapper _mapper;

        public SelCourseController(ICourseRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/mis/teacher
        [HttpGet("teacher")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeachersInfoAsync()
        {
            var teacherItems = await _repository.GetAllTeachersInfoAsync();
            
            if(teacherItems == null)
            {
                return NotFound("Not Found");
            }

            // return Ok(_mapper.Map<IEnumerable<TeacherReadDto>>(teacherItems));
            return Ok(_mapper.Map<IEnumerable<TeacherReadDto>>(teacherItems));
        }

        //GET api/mis/teacher/{tchId}
        [HttpGet("teacher/{tchId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<Teacher>> GetTeacherByIdAsync(string tchId)
        {
            // if(string.IsNullOrEmpty(tchId))
            // {
            //     return NotFound("Id is empty.");
            // }

            var teacherItem = await _repository.GetTeacherByIdAsync(tchId);
            
            if(teacherItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<TeacherReadDto>(teacherItem));
        }

        //GET api/mis/teacher/time/{ts}
        [HttpGet("teacher/time/{ts}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<Teacher>> GetTeacherByTimestampAsync(string ts)
        {
            if(string.IsNullOrEmpty(ts))
            {
                return NotFound("TimeStamp is empty");
            }

            var teacherItem = await _repository.GetTeacherByTimestampAsync(ts);
            
            if(teacherItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<TeacherReadDto>(teacherItem));
        }

        //GET api/mis/selTeacher
        [HttpGet("selTeacher")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetSelectedTeacherAsync()
        {
            var teacherItem = await _repository.GetSelectedTeacherAsync();
            
            if(teacherItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<IEnumerable<TeacherReadDto>>(teacherItem));
        }

        //GET api/mis/course
        [HttpGet("course")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<AllCourseInfo>>> GetAllCourseInfoAsync()
        {
            var courseItems = await _repository.GetAllCourseInfoAsync();
            
            if(courseItems == null)
            {
                return NotFound("Not Found");
            }

            return Ok(courseItems);
        }

        //GET api/mis/course/{crsId}
        [HttpGet("course/{crsId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<Course>> GetCourseByIdAsync(string crsId)
        {
            // if(string.IsNullOrEmpty(crsId))
            // {
            //     return NotFound("Id is empty.");
            // }

            var courseItem = await _repository.GetCourseByIdAsync(Int32.Parse(crsId));

            if(courseItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<CourseReadDto>(courseItem));
        }

        //GET api/mis/selCourse/{tchId}
        [HttpGet("selCourse/{tchId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<Course>> GetTeacherTopCourseInfoAsync(string tchId)
        {
            var courseItem = await _repository.GetTeacherTopCourseInfoAsync(tchId);
            if(courseItem == null)
            {
                return NotFound("Not Found");
            }

            return Ok(_mapper.Map<CourseReadDto>(courseItem));
        }

        //GET api/mis/stuCouse/{stuId}
        [HttpGet("stuCourse/{stuId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult<IEnumerable<StuCourse>>> GetStuCourseByIdAsync(string stuId)
        {
            if(string.IsNullOrEmpty(stuId))
            {
                return NotFound("stuId is empty.");
            }

            var stuCourseItems = await _repository.GetStuCourseByIdAsync(stuId);
            
            if(stuCourseItems == null)
            {
                return NotFound("Not Found");
            }

            return Ok(stuCourseItems);
        }

        //POST api/mis
        [HttpPost]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> AddCourseInfoAsync(SelCourseCreateDto crsDto)
        {
            var crs = _mapper.Map<SelCourseInfo>(crsDto);

            if(crs == null)
            {
                return NotFound("Input empty object");
            }

            await _repository.AddCourseInfoAsync(crs);

            return Ok("Ok");
        }

        //PUT api/mis/update
        // [HttpPut("update/{crsId}")]
        // public async Task<ActionResult> UpdateCourseInfoAsync(string crsId, Course inputCrs)
        [HttpPut("update")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> UpdateCourseInfoAsync(CourseUpdateDto crsDto)
        {
            var crs = _mapper.Map<Course>(crsDto);

            if(crs == null)
            {
                return NotFound("Input empty object");
            }

            await _repository.UpdateCourseInfoAsync(crs);

            return Ok("Ok");
        }

        //PATCH api/mis/update/{crsId}
        /*
            Body:
            [
                {
                    "op":"replace",  //add,remove,replace...
                    "path":"/remark",  //remark is a fields in tb_course(Course object)
                    "value":"testPatch"
                }
            ]
        */
        [HttpPatch("update/{crsId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> PartialUpdateCourseInfo(string crsId, [FromBody] JsonPatchDocument<CourseUpdateDto> inputPatchDto)
        {
            var courseItem = await _repository.GetCourseByIdAsync(Int32.Parse(crsId));

            if(courseItem == null)
            {
                return NotFound("NotFound");
            }

            var courseItemDto = _mapper.Map<CourseUpdateDto>(courseItem);

            //change value in inputPatchDto to value in courseItem
            inputPatchDto.ApplyTo(courseItemDto, ModelState);

            //map courseItemDto -> courseItem
            _mapper.Map(courseItemDto, courseItem);

            await _repository.SaveAsync();

            return Ok("Ok");
        }

        //DELETE api/mis/delete/{crsId}
        [HttpDelete("delete/{crsId}")]
        [Authorize]
        [EnableCors("_myAllowSpecificOrigins")]
        public async Task<ActionResult> DeleteCourseInfo(string crsId)
        {
            if(string.IsNullOrEmpty(crsId))
            {
                return NotFound("id is empty");
            }
            await _repository.DeleteCourseInfo(crsId);

            return Ok("Ok");
        }
    }
}
