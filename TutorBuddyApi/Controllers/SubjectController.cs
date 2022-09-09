using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;

namespace TutorBuddyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subject;
        public SubjectController(ISubjectService subject)
        {
            _subject = subject;
        }

        [Route("{tutorSubjectId}")]
        [HttpGet]
        public async Task<IActionResult> GetASubject([FromRoute] string tutorSubjectId)
        {
            var response = await _subject.GetASubjectDetails(tutorSubjectId);
            return StatusCode(response.StatusCode, response);
            
        }

    }
}
