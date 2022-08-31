﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TutorBuddyApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutor;
        public TutorController(ITutorService tutor)
        {
            _tutor = tutor;
        }

        [Route("get-feature-tutors/{num}")]
        [HttpGet]
        public IActionResult GetFeatureTutors(int num)
        {
            var tutors = _tutor.GetFeatureTutors(num);
            return Ok(tutors);
        }


        [Route("{Id}/add-subject")]
        [HttpPost]
        public IActionResult AddSubjectForATutors(string Id, [FromBody] IEnumerable<SubjectDTO> Subjects)
        {
            var tutors = _tutor.AddSubjectForATutor(Id, Subjects);
            return Ok(tutors);
        }
    }
}

