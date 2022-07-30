using System;
using TutorialBuddy.Core.Models;

namespace TutorBuddy.Infrastructure.Repository.Interface
{
	public interface IUserRepository
	{
        Task<User> GetUser(string userId);
        Task<bool> UpdateUser(User user);
    }
}

