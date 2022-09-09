using System;
using System.Net;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
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
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(UserManager<User> userManager, IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
            _unitOfWork = unitOfWork;
		}

       

        public async Task<ApiResponse<bool>> AddReminder(string Id, AddReminderRequestDTO reminder)
        {
            var response = new ApiResponse<bool>();
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                Reminder reminder1 = new Reminder()
                {
                    ID = Guid.NewGuid().ToString(),
                    Title = reminder.Title,
                    Note = reminder.Note,
                    StartTime = reminder.StartTime,
                    EndTime = reminder.EndTime,
                    CreatedOn = DateTime.Now,
                    User = user
                };

               
                await _unitOfWork.StudentRepository.AddRemainder(reminder1);


                //if(reminder.Provider == "IOS")
                //{
                //    var calendar = new Calendar();

                //    var icalEvent = new CalendarEvent
                //    {
                //        Summary = reminder.Title,
                //        Description = reminder.Note,
                //        Start = new CalDateTime(reminder.StartTime),
                //        End = new CalDateTime(reminder.EndTime)
                //    };

                //    calendar.Events.Add(icalEvent);
                //}

                response.StatusCode = (int)HttpStatusCode.Created;
                response.Success = true;

                return response;


            }
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = false;

            return response;

        }
    }
}

