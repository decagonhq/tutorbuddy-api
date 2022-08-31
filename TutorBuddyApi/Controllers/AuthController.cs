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


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO addTutorDTO)
        {
            var response = await _authService.RegisterUser(addTutorDTO);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            var response = await _authService.GetRegisterResource();
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDTO)
        {
            var response = await _authService.LoginUser(loginDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO confirmEmailDTO)
        {
            var response = await _authService.ConfirmEmail(confirmEmailDTO);
            return StatusCode(response.StatusCode, response); ;
        }

        [HttpPost("forgot-password")]
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


        [HttpPost("resend-otp")]
        public async Task<IActionResult> RefreshToken([FromBody] ResendOtpDTO model)
        {
            var response = await _authService.ResendOTP(model);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO model)
        {
            var response = await _authService.RefreshTokenAsync(model);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("google")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequestDTO request)
        {
            var response = await _authService.VerifyGoogleToken(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
