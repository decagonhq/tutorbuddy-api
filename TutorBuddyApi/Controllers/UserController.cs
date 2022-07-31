using System;
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
        private readonly IImageUploadService _imageUpload;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public UserController(IImageUploadService imageUpload, IUserService userService, UserManager<User> userManager)
        {
            _imageUpload = imageUpload;
            _userService = userService;
            _userManager = userManager;
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
                var user = await _userManager.FindByIdAsync(Id);

                if (user != null)
                {
                    var upload = await _imageUpload.UploadSingleImage(imageDto.ImageToUpload, "single");


                    user.AvatarUrl = upload.Url.ToString();
                    user.PublicUrl = upload.PublicId;
                    await _userManager.UpdateAsync(user);
                    return Ok(upload);
                }
                return NotFound("User not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

