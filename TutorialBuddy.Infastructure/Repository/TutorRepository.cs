using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
    public class TutorRepository : GenericRepository<Tutor>, ITutorRepository
    {
        private readonly TutorBuddyContext _context;
        public TutorRepository(TutorBuddyContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task AddTutorSubjects(Tutor tutor, IEnumerable<Subject> subjects)
        {
            if(subjects.Any())
            {
                var tutorSubjects = new TutorSubjects()
                {
                    Tutor = tutor,
                    Subjects = subjects
                };
                await _context.TutorSubjects.AddAsync(tutorSubjects);
            }
        }

        public async Task AddTutorAvailability(Tutor tutor, IEnumerable<Availability> availabilities)
        {
            if (availabilities.Any())
            {
                var tutorAvailabilities = new TutorAvailability()
                {
                    Tutor = tutor,
                    Availabilities = availabilities
                };
                await _context.TutorAvailabilities.AddAsync(tutorAvailabilities);
            }
        }
    }
}
