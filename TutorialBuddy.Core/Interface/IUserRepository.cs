using TutorialBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface IUserRepository
	{
		Task<User> GetAUserNotification(string userId);
		Task<User> GetUserByRefreshToken(string token, string userId);
	}
}

