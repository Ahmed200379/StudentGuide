using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Department;
using StudentGuide.BLL.Dtos.Material;
using StudentGuide.BLL.Services.Departments;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [Authorize(Roles = "Student,Admin")]
        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                IEnumerable<DepartmentReadDto> AllDepartmentFromDb = await _departmentService.GetAllDepartment();
                if (AllDepartmentFromDb is null)
                {
                    return BadRequest();
                }
                return Ok(AllDepartmentFromDb);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [Authorize(Roles = "Student,Admin")]
        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        public async Task<ActionResult<DepartmentReadDto>> GetDepartmentById(String id)
        {
            try
            {
                DepartmentReadDto? department = await _departmentService.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("DashBoard/AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody]DepartmentAddDto newDepartment)
        {
            try
            {
                bool isAdded = await _departmentService.AddDepartment(newDepartment);
                if (isAdded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Student,Admin")]
        [HttpPut]
        [Route("DashBoard/UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentEditDto departmentEditDto)
        {
            if (!string.IsNullOrWhiteSpace(departmentEditDto.NameOfDepartment) && !string.IsNullOrWhiteSpace(departmentEditDto.DepartmentCode))
            {
                try
                {
                    bool isUpdated = await _departmentService.UpdateDepartment(departmentEditDto);
                    if (isUpdated)
                        return Ok();
                    return BadRequest("Faild to update");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return BadRequest("eaither Code or name can not be null");
            }
          
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("Dashboard/DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(string id)
        {

            try
            {
                bool isDeleted = await _departmentService.DeleteDepartment(id);
                return isDeleted ? NoContent() : BadRequest("Failed to delete Department.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
