﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IServiceProvider provider)
        {
        }

        public async Task<ApiResponse<string>> AddStudent(AddStudentDTO addStudentDTO)
        {
            var response = new ApiResponse<string>();

            response.StatusCode = (int)HttpStatusCode.Created;
            response.Message = "Student created successfully! Check your mail to verify your account.";
            response.Data = string.Empty;
            response.Success = true;

            return response;
        }

        public async Task<ApiResponse<string>> AddTutor(AddTutorDTO addTutorDTO)
        {
            var response = new ApiResponse<string>();

            response.StatusCode = (int)HttpStatusCode.Created;
            response.Message = "Student created successfully! Check your mail to verify your account.";
            response.Data = string.Empty;
            response.Success = true;

            return response;
        }
    }
}