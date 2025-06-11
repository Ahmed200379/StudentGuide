using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Result;
using StudentGuide.BLL.Services.Results;
using Microsoft.AspNetCore.Authorization;
namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultsService _resultsService;
        public ResultController(IResultsService resultsService)
        {
            _resultsService = resultsService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddResult")]
        public async Task<IActionResult> AddResult([FromBody] IEnumerable<ResultAddDto> results)
        {
            try
            {
                await _resultsService.AddResult(results);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllResultForAdmin")]
        public async Task<IActionResult> GetAllResultForAdmin([FromQuery] string code, [FromQuery] string semester)
        {
            try
            {
                var result = await _resultsService.GetAllResultForAdmin(code, semester);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Student,Admin")]
        [HttpGet("GetAllResultForSpecificStudent")]
        public async Task<IActionResult> GetAllResultForSpecificStudent([FromQuery] string code, [FromQuery] string semester)
        {
            try
            {
                var result = await _resultsService.GetAllResultForSpecificStudent(code, semester);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllResultsForAllStudents")]
        public async Task<IActionResult> GetAllResultsForAllStudents( [FromQuery]string semester)
        {
            try
            {
                var result = await _resultsService.GetAllResultsForAllStudents(semester);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddResultWithExcel")]
        public async Task<IActionResult> AddResultWithExcel(IFormFile file)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    await _resultsService.AddResultWithExcel(stream);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
