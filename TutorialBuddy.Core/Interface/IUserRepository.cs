using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface IUserRepository
	{
		Task<User> GetAUserNotification(string userId);
		Task<User> GetUserByRefreshToken(string token, string userId);
		Task AddUserAreaOfInterestA(User user, IEnumerable<Subject> subjects);
	}
}

