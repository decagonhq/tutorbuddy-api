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

        public IEnumerable<FeatureTutorDTO> GetFeatureTutors(int num)
        {
            var tutors =  _context.Tutors
                         .Include(x => x.TutorSubjects.Where(x => x.TutorID != null))
                         .Include(x => x.User)
                         .ToList();
            List<FeatureTutorDTO> result = new List<FeatureTutorDTO>();

           foreach(var item in tutors)
           {
                FeatureTutorDTO res = new FeatureTutorDTO();
                res.Id = item.UserId;
                res.Fullname = item.User.FirstName + " " + item.User.LastName;
                res.Avatar = item.User.AvatarUrl;
                foreach (var element in item.TutorSubjects)
                {
                    int rateSum = element.Sessions.Sum(x => x.RateTutor);
                    int rateCount = element.Sessions.Count();
                    double calrate = rateSum / rateCount;
                    res.Rate = (int)Math.Round(calrate, 1);

                }

               
           }
            return result.OrderByDescending(x => x.Rate).Take(num);
        }




    }
}
