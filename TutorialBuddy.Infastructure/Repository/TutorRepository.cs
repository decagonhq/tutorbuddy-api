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
            if (subjects.Any())
            {
                foreach (var item in subjects)
                {
                    var tutorSubjects = new TutorSubject()
                    {
                        SubjectID = item.ID,
                        TutorID = tutor.UserId

                    };
                    await _context.TutorSubjects.AddAsync(tutorSubjects);
                }
            }
        }

        public async Task AddTutorAvailability(Tutor tutor, IEnumerable<Availability> availabilities)
        {
            foreach (var item in availabilities)
            {
                var tutorAvailable = new TutorAvaliability()
                {
                    AvailabilityID = item.ID,
                    TutorID = tutor.UserId

                };
                await _context.TutorAvaliabilities.AddAsync(tutorAvailable);
            }
        }
    }
}
