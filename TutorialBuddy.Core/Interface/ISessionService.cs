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
        Task<ApiResponse<PaginationModel<IEnumerable<StudentSessionResponseDTO>>>>GetAllSessionForStudent(string studentId, int pageSize, int pageNumber);
        Task<ApiResponse<PaginationModel<IEnumerable<TutorSessionResponseDTO>>>> GetAllSessionForTutor(string tutorId, int pageSize, int pageNumber);
        Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO session, string Id);
        Task<ApiResponse<bool>> CommentOnSession(string sessionId, CreateCommentDTO createComment, string commentFor);
        Task<ApiResponse<bool>> RateSession(string sessionId, int ratings, string ratingsFor);
        
    }
}
