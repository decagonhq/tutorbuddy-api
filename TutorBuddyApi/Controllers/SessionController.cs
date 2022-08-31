
﻿using Microsoft.AspNetCore.Authorization;
﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TutorBuddy.Core.DTOs;
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
        public async Task<IActionResult> AddSession([FromBody] CreateSessionDTO createSession)
        {
            var result = await sessionService.AddSession(createSession);
            return Ok(result);
        }

        /// <summary>
        /// Update a session
        /// </summary>
        /// <param name="updateSession"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPatch]
        public async Task<IActionResult> UpdateSession([FromBody] UpdateSessionDTO updateSession)
        {
            var result = await sessionService.UpdateSession(updateSession);
            return Ok(result);
        }

        /// <summary>
        /// Get all session for a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/student")]
        [HttpGet]
        public async Task<IActionResult> GetAllSession([FromRoute] string id)
        {
            var result = await sessionService.GetAllSession(id);
            return Ok(result);
        }

        /// <summary>
        /// Get all session for tutor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}/tutor")]
        [HttpGet]
        public async Task<IActionResult> GetAllSessionTutor([FromRoute] string id)
        {
            var result = await sessionService.GetAllSessionTutor(id);
            return Ok(result);
        }

        /// <summary>
        /// Students comments on a session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [Route("{id}/student-comment")]
        [HttpPost]
        public async Task<IActionResult> CommentOnSession([FromRoute] string id, [FromBody] CreateCommentDTO commentDTO)
        {
            var result = await sessionService.CommentOnSession(id, commentDTO, User);
            return Ok(result);
        }

        /// <summary>
        /// Tutor comments on a session
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [Route("{id}/tutor-comment")]
        [HttpPost]
        public async Task<IActionResult> CommentOnSessionTutor([FromRoute] string id, [FromBody] CreateCommentDTO commentDTO)
        {
            var result = await sessionService.CommentOnSessionTutor(id, commentDTO, User);
            return Ok(result);
        }

        /// <summary>
        /// Rate a session for tutor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        [Route("session/{id}/rate-tutor")]
        [HttpPost]
        public async Task<IActionResult> RateSession([FromRoute] string id, [FromBody] RatingsDTO ratings)
        {
            var result = await sessionService.RateSession(id, ratings.Ratings, "tutor");
            return Ok(result);
        }

        /// <summary>
        /// Rate a session for student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        [Route("session/{id}/rate-student")]
        [HttpPost]
        public async Task<IActionResult> RateSessionStudent([FromRoute] string id, [FromBody] RatingsDTO ratings)
        {
            var result = await sessionService.RateSession(id, ratings.Ratings, "student");
            return Ok(result);
        }

        public class RatingsDTO
        {
            [Range(0, 5)]
            public int Ratings { get; set; }
        }
    }
}