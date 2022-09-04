using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
    public class TutorSubjectRepository : GenericRepository<TutorSubject>, ITutorSubjectRepository
    {
        private readonly TutorBuddyContext dbContext;

        public TutorSubjectRepository(TutorBuddyContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<TutorSubject>> GetAllSubjectATutor(string tutorSubjectId)
        {
            var result = await dbContext.TutorSubjects
                                .Where(x => x.TutorID == tutorSubjectId)
                                .ToListAsync();
            return result;
        }

        public async Task<TutorSubject> GetDetail(string Id)
        {
            var ts = await this.dbContext.TutorSubjects.FirstOrDefaultAsync(ts => ts.ID == Id);
            return ts;
        }
    }
}