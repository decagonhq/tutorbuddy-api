using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
    public interface ISessionRepository
    {
        Task<bool> AddSession(Session session, User student);
        Task<bool> UpdateSession(Session session);
        Task DeleteSession(Session session, bool mode = false);
        Task<IEnumerable<Session>> GetAllSessions(string studentId);
        Task<Session> FindSessionByIdAsync(string id);
        Task<ApiResponse<bool>> SaveComments(StudentComment comment);

        Task<IEnumerable<Session>> GetAllSessionsForTutor(string tutorId);
    }
}
