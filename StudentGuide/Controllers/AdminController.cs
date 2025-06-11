
using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Admin;
using Microsoft.AspNetCore.Authorization;
using StudentGuide.BLL.Dtos.Email;
using StudentGuide.BLL.Services.ManagementService;
using StudentGuide.DAL.Data.Models;

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IManagementService _managementService;
        public AdminController(IManagementService managementService)
        {
            _managementService = managementService;
        }
        [HttpPost]
        [Route("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] AdminAddDto newAdmin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _managementService.AddAdmin(newAdmin);
                if (result.IsSuccessed == false)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SendMessages")]
        public async Task<IActionResult> SendMessages([FromBody] EmailRequestDto email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _managementService.SendMessageToEmail(email);
                if (result.IsSuccessed == false)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SendDepartmentRegisterationMessages")]
        public async Task<IActionResult> SendDepartmentRegisterationMessages([FromBody] EmailRequestDto email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _managementService.SendDepartmentNotificationEmail(email);
                if (result.IsSuccessed == false)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
