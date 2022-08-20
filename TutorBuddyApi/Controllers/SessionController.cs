﻿using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;

namespace TutorBuddyApi.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        /// <summary>
        /// Add a session
        /// </summary>
        /// <param name="createSession"></param>
        /// <returns></returns>
        [Route("session")]
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
        /// Update a session
        /// </summary>
        /// <param name="updateSession"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllSession([FromRoute] string id)
        {
            var result = await sessionService.GetAllSession(id);
            return Ok(result);
        }
    }
}