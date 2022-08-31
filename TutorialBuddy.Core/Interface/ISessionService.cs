using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        Task<IEnumerable<Session>> GetAllSessionTutor(string tutorId);
        Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO session);
        Task<ApiResponse<bool>> CommentOnSession(string sessionId, CreateCommentDTO createComment, ClaimsPrincipal claimsPrincipal);
        Task<ApiResponse<bool>> RateSession(string sessionId, int ratings, string ratingsFor = "tutor");
        Task<ApiResponse<bool>> CommentOnSessionTutor(string id, CreateCommentDTO commentDTO, ClaimsPrincipal user);
    }
}
