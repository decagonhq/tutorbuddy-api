using System;
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
        Task<ApiResponse<AddResponseDTO>> AddStudent(AddStudentDTO addStudentDTO);
        Task<ApiResponse<AddResponseDTO>> AddTutor(AddTutorDTO addTutorDTO);
        Task<ApiResponse<AddResponseDTO>> LoginUser(LoginUserDTO loginUserDTO);
        Task<ApiResponse<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<ApiResponse<ConfirmEmailResponseDTO>> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO);
    }
}
