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
     
        Task<ApiResponse<string>> RegisterUser(RegisterDTO model);
        Task<ApiResponse<GetRegisterResponseDTO>> GetRegisterResource();
        Task<ApiResponse<CredentialResponseDTO>> LoginUser(LoginUserDTO loginUserDTO);
        Task<ApiResponse<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<ApiResponse<string>> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO);
        Task<ApiResponse<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequestDTO token);
        Task<ApiResponse<CredentialResponseDTO>> VerifyGoogleToken(GoogleLoginRequestDTO google)
    }
}
