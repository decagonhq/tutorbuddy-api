using System;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface IUserService
	{
		Task<ApiResponse<UserResponseDTO>> GetUser(string Id);

        Task<ApiResponse<string>> UploadUserAvatarAsync(string Id, UploadImageDTO imageDto);

        Task<ApiResponse<string>> UpdatePasswordAsync(UpdatePasswordDTO model);

        Task<ApiResponse<string>> UpdateAsync(UpdateUserDTO model);
    }
}

