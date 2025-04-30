using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Course;
using StudentGuide.BLL.Dtos.Material;
using StudentGuide.BLL.Services.Courses;

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpPost("DashBoard/AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] CourseAddDto newCourse)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                 await _courseService.AddCourse(newCourse);
                return Ok(new { message = "Course Added Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message= ex.InnerException?.Message ?? ex.Message });

            }
        }
        [HttpPut("DashBoard/UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseEditDto courseEditDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _courseService.EditCourse(courseEditDto);
                return Ok(new { message= "Course Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] String keyword)
        {
            try
            {
                var materials = await _courseService.Search(keyword);
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
         [HttpDelete]
        [Route("Dashboard/DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseService.DeleteCourse(id);
                return Ok(new {message="Course deleted successfuly"});
                
            }catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllCoursesInPagnation/{page}/{countPerPage}")]
        public async Task<IActionResult> GetAllCoursesInPagnation(int page, int countPerPage)
        {
            try
            {
                var courses = await _courseService.GetAllCoursesInPagnation(page, countPerPage);
                    return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourseById(String id)
        {
            try
            {
                if(!ModelState.IsValid )
                {
                    return BadRequest(ModelState);
                }
                CourseReadDto? course = await _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }
        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                IEnumerable<CourseReadDto> allCourses = await _courseService.GetAllCourses();
                return Ok(allCourses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }
        [HttpGet]
        [Route("GetAllAvalibleCourses")]
        public async Task<IActionResult> GetAllAvailableCourses(string code)
        {
            try
            {
               if(!ModelState.IsValid )
                {
                    return BadRequest(ModelState);
                }
               var availableCourses= await _courseService.GetAllCoursesForStudent(code);
                return Ok(availableCourses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
