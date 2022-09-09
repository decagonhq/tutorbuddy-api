using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface IStudentRepository
	{
        Task AddRemainder(Reminder reminder);

    }
}

