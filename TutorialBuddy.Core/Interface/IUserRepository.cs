using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface IUserRepository
	{
		Task<User> GetAUserNotification(string userId);
		Task<User> GetUserByRefreshToken(string token, string userId);
		Task<User> GetAUser(string Id, string role);
        Task<User> GetAUserReminders(string userId);


    }
}

