using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;

namespace TutorBuddyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _student;
        public StudentController(IStudentService student)
        {
            _student = student;
        }


        [Route("{Id}/add-reminder")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPost]
        public async Task<IActionResult> AddReminder(string Id, [FromBody] IEnumerable<AddReminderRequestDTO> reminder)
        {
            //var response = await _tutor.AddAvaliabilityForATutor(Id, availabilities);
            //return StatusCode(response.StatusCode, response);

            return Ok();
        }
    }
}
