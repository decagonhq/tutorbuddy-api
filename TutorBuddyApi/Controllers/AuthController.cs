using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorBuddyApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IServiceProvider provider)
        {
            _authService = provider.GetRequiredService<IAuthenticationService>();
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> StudentRegister([FromBody] AddStudentDTO addStudentDTO)
        {
            var response = await _authService.AddStudent(addStudentDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("register-tutor")]
        public async Task<IActionResult> TutorRegister([FromBody] AddTutorDTO addTutorDTO)
        {
            var response = await _authService.AddTutor(addTutorDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDTO)
        {
            var response = await _authService.LoginUser(loginDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO confirmEmailDTO)
        {
            var response = await _authService.ConfirmEmail(confirmEmailDTO);
            return StatusCode(response.StatusCode, response); ;
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = await _authService.ForgotPassword(forgotPasswordDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var response = await _authService.ResetPassword(resetPasswordDTO);
            return StatusCode(response.StatusCode, response);
        }
    }
}
