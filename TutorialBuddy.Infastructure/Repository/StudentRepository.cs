using System;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
	public class StudentRepository : IStudentRepository
    {
        private readonly TutorBuddyContext dbContext;

        public StudentRepository(TutorBuddyContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddRemainder(Reminder reminder)
        {
            var rem = await dbContext.Reminders.AddAsync(reminder);

            await dbContext.SaveChangesAsync();

        }
    }
}

