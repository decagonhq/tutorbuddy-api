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

        public async Task<TutorSubject> GetDetail(string Id)
        {
            var ts = await this.dbContext.TutorSubjects.FirstOrDefaultAsync(ts => ts.ID == Id);
            return ts;
        }
    }
}