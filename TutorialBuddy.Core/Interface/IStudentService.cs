using System;
using TutorBuddy.Core.DTOs;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface IStudentService
	{
        Task<ApiResponse<bool>> AddReminder(string Id, AddReminderRequestDTO reminder);

    }
}

