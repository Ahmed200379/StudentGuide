using Microsoft.AspNetCore.Mvc;
using StudentGuide.BLL.Dtos.Account;
using Microsoft.AspNetCore.Authorization;
using StudentGuide.BLL.Services.AccountService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto newUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {                   var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                      .Select(e => e.ErrorMessage)
                                                      .ToList();
                    return BadRequest(errors);
                }
                var result = await _accountService.Register(newUser);
                if (result.IsAuthenticated == false)
                {
                    return BadRequest(result.Message);
                }
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest( ex.InnerException);
            }
           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _accountService.Login(user);
                if (result.IsAuthenticated == false)
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

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPaaword([FromBody] EmailReadDto emailReadDto)
        {
            if(emailReadDto.Email == null)
            {
                return BadRequest("You should enter a message");
            }
            var response= await _accountService.ForgetPassword(emailReadDto);
            return Ok(response);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _accountService.ResetPassword(resetPasswordDto);
            return Ok(response);
        }
        [HttpPost("CheckCode")]
        public async Task<IActionResult> CheckCode([FromBody] CheckCodeDto checkCodeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _accountService.ValidateCode(checkCodeDto);
            return Ok(response);
        }
    }
}

