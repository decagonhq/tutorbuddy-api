using System;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface IUserService
	{
		Task<ApiResponse<User>> GetUser(string Id);

        Task<ApiResponse<bool>> UploadUserAvatar(string Id, UploadImageDTO imageDto);

        Task<ApiResponse<string>> UpdatePassword(UpdatePasswordDTO model);

        Task<ApiResponse<string>> Update(UpdateUserDTO model);
    }
}

