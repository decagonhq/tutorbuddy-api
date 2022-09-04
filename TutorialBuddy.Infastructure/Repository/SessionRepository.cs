using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;
using TutorialBuddy.Core;

namespace TutorBuddy.Infrastructure.Repository
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly TutorBuddyContext dbContext;

        public SessionRepository(TutorBuddyContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddSession(Session session)
        {
            await Add(session);
            await dbContext.SaveChangesAsync();
            
        }

        /// <summary>
        /// Deprecates or truly deletes from the store depending on the mode flag
        /// </summary>
        /// <param name="session">Session to delete/depricate</param>
        /// <param name="mode">True - Delete from db, False - just deprecate</param>
        /// <returns></returns>
        public async Task DeleteSession(Session session, bool mode = false)
        {
            if (mode)
            {
                dbContext.Sessions.Remove(session);
            }
            else
            {
                session.IsDepricated = true;
                dbContext.Sessions.Update(session);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<Session> FindSessionByIdAsync(string id)
        {
            var res = await dbContext.Sessions.FirstOrDefaultAsync(s => s.IsDepricated == false && s.ID == id);
            return res;
        }

        public async Task<User> GetAllSessionsForAstudent(string studentId)
        {
            var sessions = await dbContext.Users
                .Where(s => s.Id == studentId)
                .Include(x => x.Sessions)
                .SingleOrDefaultAsync();
            return sessions;
        }

        public async Task<Tutor> GetAllSessionsForTutor(string tutorId)
        {
            var sessions = await dbContext.Tutors
                .Where(s => s.UserId == tutorId)
                .Include(session => session.TutorSubjects)
                    .ThenInclude(x => x.Sessions)
                .SingleOrDefaultAsync();
            return sessions;
        }

        public async Task<bool> UpdateSession(Session session)
        {
            dbContext.Sessions.Update(session);
            await dbContext.SaveChangesAsync();
            return true;
        }

        
    }
}
