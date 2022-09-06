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
        Task AddSession(Session session);
        Task<bool> UpdateSession(Session session);
        Task DeleteSession(Session session, bool mode = false);
        Task<User> GetAllSessionsForAstudent(string studentId);
        Task<Session> FindSessionByIdAsync(string id);
        Task<Tutor> GetAllSessionsForTutor(string tutorId);
    }
}
