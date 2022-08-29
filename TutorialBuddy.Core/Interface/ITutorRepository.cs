using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
    public interface ITutorRepository : IGenericRepository<Tutor>
    {
        Task AddTutorSubjects(Tutor tutor, IEnumerable<Subject> subjects);
        Task AddTutorAvailability(Tutor tutor, IEnumerable<Availability> availabilities);
        Task<IEnumerable<Tutor>> GetFeatureTutors(int num);
    }
}
