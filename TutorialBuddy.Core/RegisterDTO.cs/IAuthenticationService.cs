﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBuddy.Core.DTOs;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<string>> AddStudent(AddStudentDTO addStudentDTO);
        Task<ApiResponse<string>> AddTutor(AddTutorDTO addTutorDTO);
        Task<ApiResponse<CredentialResponseDTO>> LoginUser(LoginUserDTO loginUserDTO);
        Task<ApiResponse<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<ApiResponse<string>> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO);
    }
}