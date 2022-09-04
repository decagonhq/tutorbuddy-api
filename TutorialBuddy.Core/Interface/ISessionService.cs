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
        Task<ApiResponse<IEnumerable<StudentSessionResponseDTO>>> GetAllSessionForStudent(string studentId);
        Task<ApiResponse<IEnumerable<TutorSessionResponseDTO>>> GetAllSessionForTutor(string tutorId);
        Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO session);
        Task<ApiResponse<bool>> CommentOnSession(string sessionId, CreateCommentDTO createComment, string commentFor);
        Task<ApiResponse<bool>> RateSession(string sessionId, int ratings, string ratingsFor);
        
    }
}
