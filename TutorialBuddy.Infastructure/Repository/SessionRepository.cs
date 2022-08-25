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

        public async Task<bool> AddSession(Session session, User student)
        {
            session.Student = student;
            dbContext.Sessions.Add(session);
            await dbContext.SaveChangesAsync();
            return true;
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

        public async Task<IEnumerable<Session>> GetAllSessions(string studentId)
        {
            var sessions = await dbContext.Sessions
                .AsNoTracking()
                .Include(session => session.Student)
                .Where(s => s.IsDepricated == false && s.Student.Id == studentId)
                .ToListAsync();
            return sessions;
        }

        public async Task<bool> UpdateSession(Session session)
        {
            dbContext.Sessions.Update(session);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ApiResponse<bool>> SaveComments(StudentComment comment)
        {
            var _res = new ApiResponse<bool>();
            await _dbContext.StudentComments.AddAsync(comment);
            var res = await _dbContext.SaveChangesAsync();
            _res.Success = true;
            if (res > 0) return _res;
            _res.Success = false;
            return _res;
        }
    }
}
