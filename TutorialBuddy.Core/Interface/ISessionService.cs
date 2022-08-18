using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
    public interface ISessionService
    {
        Task<ApiResponse<bool>> AddSession(CreateSessionDTO createSession);
        Task RemoveSession(string sessionId);

        Task<IEnumerable<Session>> GetAllSession(string studentId);
        Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO session);
    }
}
