using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.DTOs;
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

        public async Task<IEnumerable<Tutor>> GetFeatureTutors(int num)
        {
            var tutors =  _context.Tutors
                         .Include(x => x.TutorSubjects)
                            .ThenInclude(x => x.Sessions)
                        .Include(x => x.User)
                        .ToList();
            List<FeatureTutorDTO> res = new List<FeatureTutorDTO>();

            var rates = _context.Sessions.Select(x => x.RateTutors.Sum(x => x.Rate));

            foreach (var item in tutors)
            {
                foreach (var x in item.TutorSubjects)
                {
                    //var txt = x.Sessions.Where(x => x.TutorSubject.ID == );
                }
            }
            return tutors;
        }

    }
}
