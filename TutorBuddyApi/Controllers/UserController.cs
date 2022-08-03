﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorBuddyApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {

            _userService = userService;

        }

        /// <summary>
        /// Endpoint is to upload a single image on the user profile 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="imageDto"></param>
        /// <returns></returns>
        [HttpPatch("Id/upload-image")]
        public async Task<IActionResult> UploadImage(string Id, [FromForm] UploadImageDTO imageDto)
        {
            try
            {
                var result = await _userService.UploadUserAvatarAsync(Id, imageDto);
                return StatusCode(result.StatusCode, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update user password 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO model)
        {
            try
            {
                var result = await _userService.UpdatePasswordAsync(model);
                return StatusCode(result.StatusCode, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Id")]
        public async Task<IActionResult> GetUser(string Id)
        {
            try
            {
                var result = await _userService.GetUser(Id);
                return StatusCode(result.StatusCode, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Id")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserDTO model)
        {
            try
            {
                var result = await _userService.UpdateAsync(model);
                return StatusCode(result.StatusCode, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
