using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using static Google.Apis.Requests.BatchRequest;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorBuddyApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutor;
        public TutorController(ITutorService tutor)
        {
            _tutor = tutor;
        }

        [Route("get-feature-tutors/{num}")]
        [HttpGet]
        public async Task<IActionResult> GetFeatureTutors(int num)
        {
            var response = await _tutor.GetFeatureTutors(num);
            return StatusCode(response.StatusCode, response);
        }


        [Route("{Id}/add-subject")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPost]
        public async Task<IActionResult> AddSubjectForATutors(string Id, [FromBody] IEnumerable<SubjectDTO> Subjects)
        {
            var response = await _tutor.AddSubjectForATutor(Id, Subjects);
            return StatusCode(response.StatusCode, response);
        }

        [Route("{Id}/add-avaliability")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPost]
        public async Task<IActionResult> AddAvaliabilityForATutors(string Id, [FromBody] IEnumerable<AvailabilityDTO> availabilities)
        {
            var response = await _tutor.AddAvaliabilityForATutor(Id, availabilities);
            return StatusCode(response.StatusCode, response);
        }

        [Route("get-all-subject-with-categories/{pageNumber}")]
        [HttpGet]
        public IActionResult GetAllSubjectWithCategories(int pageNumber)
        {
            var tutors = _tutor.GetRecommendedSubject(pageNumber);
            return Ok(tutors);
        }
    }
}

