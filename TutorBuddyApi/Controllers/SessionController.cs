
﻿using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using SendGrid;
using System.ComponentModel.DataAnnotations;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;

namespace TutorBuddyApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        /// <summary>
        /// Create a session
        /// </summary>
        /// <param name="createSession"></param>
        /// <returns></returns>
       
        [HttpPost]
        [Authorize(Policy = "RequireStudentOnly")]
        public async Task<IActionResult> AddSession([FromBody] CreateSessionDTO createSession)
        {
            var result = await sessionService.AddSession(createSession);
            return StatusCode(result.StatusCode, result);
            
        }

        /// <summary>
        /// Update a session
        /// </summary>
        /// <param name="updateSession"></param>
        /// <returns></returns>
        [Route("{id}")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSession([FromBody] UpdateSessionDTO updateSession)
        {
            var result = await sessionService.UpdateSession(updateSession);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get all session for a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/student")]
        [Authorize(Policy = "RequireStudentOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllSession([FromRoute] string id)
        {
            var result = await sessionService.GetAllSessionForStudent(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get all session for tutor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/tutor")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpGet]
        public async Task<IActionResult> GetAllSessionTutor([FromRoute] string id)
        {
            var result = await sessionService.GetAllSessionForTutor(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Students comments on a session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [Route("{id}/student-comment")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPost]
        public async Task<IActionResult> CommentOnSession([FromRoute] string id, [FromBody] CreateCommentDTO commentDTO)
        {
            var result = await sessionService.CommentOnSession(id, commentDTO, UserRole.Student.ToString());
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Tutor comments on a session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [Route("{id}/tutor-comment")]
        [Authorize(Policy = "RequireStudentOnly")]
        [HttpPost]
        public async Task<IActionResult> CommentOnSessionTutor([FromRoute] string id, [FromBody] CreateCommentDTO commentDTO)
        {
            var result = await sessionService.CommentOnSession(id, commentDTO, UserRole.Tutor.ToString());
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Rate a session for tutor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        [Route("{id}/rate-tutor")]
        [Authorize(Policy = "RequireStudentOnly")]
        [HttpPost]
        public async Task<IActionResult> RateSession([FromRoute] string id, [FromBody] RatingsDTO ratings)
        {
            var result = await sessionService.RateSession(id, ratings.Ratings, UserRole.Tutor.ToString());
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Rate a session for student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        [Route("{id}/rate-student")]
        [Authorize(Policy = "RequireTutorOnly")]
        [HttpPost]
        public async Task<IActionResult> RateSessionStudent([FromRoute] string id, [FromBody] RatingsDTO ratings)
        {
            var result = await sessionService.RateSession(id, ratings.Ratings, UserRole.Student.ToString());
            return StatusCode(result.StatusCode, result);
        }

        public class RatingsDTO
        {
            [Range(0, 5)]
            public int Ratings { get; set; }
        }
    }
}