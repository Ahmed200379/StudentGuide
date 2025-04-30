using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using StudentGuide.BLL.Dtos.Student;
using StudentGuide.BLL.Services.Students;

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
       private readonly IStudentService _studentService;
       public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpPost]
        [Route("DashBoard/AddNewStudent")]
        public async Task<IActionResult> AddNewStudent([FromBody]StudentAddDto studentAddDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _studentService.AddNewStudent(studentAddDto);
                return Ok("Successfully Added new Student");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpPost]
        [Route("DashBoard/EnrollCourses")]
        public async Task<IActionResult> AddNewStudent([FromBody]StudentErollDto courses)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _studentService.EnrollCourses(courses);
                return Ok("Successfully enrolled courses");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpPut]
        [Route("DashBoard/UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody]StudentEditDto studentEditDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               await _studentService.EditStudent(studentEditDto);
                return Ok("Successfuly Update student");
            }catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
        [HttpGet]
        [Route("DashBoard/GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var allStudents = await _studentService.GetAllSudentsWithCount();
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
           
        }
        [HttpGet]
        [Route("DashBoard/GetAllStudentsInpagnation/{page}/{countPerPage}")]
        public async Task<IActionResult> GetAllStudentsInPagnation(int page, int countPerPage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var allStudents = await _studentService.GetAllStudentsInPagnation(page,countPerPage);
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }
        [HttpGet]
        [Route("DashBoard/GetByIdForAdmin/{code}")]
        public async Task<IActionResult> GetByIdForAdmin(string code)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var student = await _studentService.GetByIdForAdmin(code);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }
        [HttpGet]
        [Route("DashBoard/GetByIdForStudent/{code}")]
        public async Task<IActionResult> GetByIdForStudent(string code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var student = await _studentService.GetByIdForStudent(code);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }

        }
        [HttpDelete]
        [Route("DashBoard/DeleteStudent")]
        public async Task<IActionResult> DeleteById(string code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _studentService.Delete(code);
                return Ok("Successfully deleted the Student");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}
