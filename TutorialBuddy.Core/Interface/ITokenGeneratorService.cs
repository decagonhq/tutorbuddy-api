using TutorialBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
    public interface ITokenGeneratorService
    {
        Task<string> GenerateToken(User user);
        Guid GenerateRefreshToken();
    }
}
