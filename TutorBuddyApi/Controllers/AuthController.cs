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

        [HttpPost("add-student")]
        public async Task<IActionResult> StudentRegister([FromBody] AddStudentDTO addStudentDTO)
        {
            var response = await _authService.AddStudent(addStudentDTO);
            if (response.Success) return Created("", response);
            return BadRequest(response);
        }
    }
}
