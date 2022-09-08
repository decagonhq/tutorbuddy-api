using System;
using Microsoft.AspNetCore.Identity;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
	public class StudentService : IStudentService
    {
        private readonly UserManager<User> _userManager;
        public StudentService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

        public Task<ApiResponse<bool>> AddReminder(string Id, AddReminderRequestDTO reminder)
        {
            throw new NotImplementedException();
        }

        //public async Task<ApiResponse<bool>> AddReminder(string Id, AddReminderRequestDTO reminder)
        //{
        //	var user = await _userManager.FindByIdAsync(Id);

        //	if(user != null)
        //	{
        //		Reminder reminder1 = new Reminder()
        //		{
        //			ID = Guid.NewGuid().ToString(),
        //			Title = reminder.Title,
        //			Note = reminder.Note,
        //			StartTime = reminder.StartTime,
        //			CreatedOn = DateTime.Now
        //		};

        //		user.Reminders.Add(reminder1)

        //          }
        //}
    }
}

