
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
    }
}

