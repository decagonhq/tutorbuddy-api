
using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
	public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly TutorBuddyContext _context;
        public SubjectRepository(TutorBuddyContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }


        public async Task<IEnumerable<Subject>> GetAllSubjectAsync()
        {
            var subject = await GetAllRecord();

            return subject;
        }

        public async Task<Subject> GetASubjectAsync(string subjectId)
        {
            var subject = await GetARecord(subjectId);

            return subject;
        }

        public async Task<IEnumerable<Subject>> GetAllRecommendSubjectAsync()
        {
            var subject = await _context.Subjects
                          .Include(x => x.TutorSubjects)
                          .ToListAsync();

            return subject;
        }
    }
}

