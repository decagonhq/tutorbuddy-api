using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IUserRepository userRepository;
        private readonly ITutorSubjectRepository tutorSubjectRepository;
        private readonly UserManager<User> userManager;

        public SessionService(ISessionRepository sessionRepository,
            IUserRepository userRepository,
            ISubjectRepository subjectRepository,
            ITutorSubjectRepository tutorSubjectRepository,
            UserManager<User> userManager
            )
        {
            this.sessionRepository = sessionRepository;
            this.userRepository = userRepository;
            this.tutorSubjectRepository = tutorSubjectRepository;
            this.userManager = userManager;
        }

        public async Task<ApiResponse<bool>> AddSession(CreateSessionDTO createSession)
        {
            var response = new ApiResponse<bool>();
            var student = await userRepository.GetAUser(createSession.StudentId!, "student");

            response.Message = "Student not found";
            if (student == null) return response;

            var session = new Session
            {
                CreatedOn = DateTime.Now,
                Startime = createSession.StartTime,
                EndTime = createSession.EndTime,
                Status = TutorialBuddy.Core.Enums.SessionStatus.pending,
                TutorSubject = await tutorSubjectRepository.GetDetail(createSession.TutorSubjectId!),
                Student = student,
            };

            var res = await sessionRepository.AddSession(session, student);

            response.Success = res;
            return response;
        }

        public async Task<ApiResponse<bool>> CommentOnSession(string sessionId, CreateCommentDTO createComment, ClaimsPrincipal claimsPrincipal)
        {
            var response = new ApiResponse<bool>();
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var student = await userRepository.GetAUser(user.Id, "student");
            var session = await sessionRepository.FindSessionByIdAsync(sessionId);

            response.Message = "Session not found";
            if (session == null) return response;

            response.Message = "Student not found";
            if (student == null) return response;

            if(session.Status != SessionStatus.completed)
            {
                response.Message = "You can't comment at this point";
                return response;
            }

            var comment = new StudentComment
            {
                Author = $"{user.FirstName} {user.LastName}",
                Comment = createComment.Comment,
                Session = session,
            };

            var result = await sessionRepository.SaveComments(comment);

            response.Success = true;
            response.Message = "Comment created successfully";
            if (result.Success) return response;
            response.Success = true;
            response.Message = "Failed to create comment";
            return response;
        }

        public async Task<ApiResponse<bool>> CommentOnSessionTutor(string sessionId, CreateCommentDTO createComment, ClaimsPrincipal claimsPrincipal)
        {
            var response = new ApiResponse<bool>();
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var tutor = await userRepository.GetAUser(user.Id, "tutor");
            var session = await sessionRepository.FindSessionByIdAsync(sessionId);

            response.Message = "Session not found";
            if (session == null) return response;

            response.Message = "Tutor not found";
            if (tutor == null) return response;

            if (session.Status != SessionStatus.completed)
            {
                response.Message = "You can't comment at this point";
                return response;
            }

            var comment = new StudentComment
            {
                Author = $"{user.FirstName} {user.LastName}",
                Comment = createComment.Comment,
                Session = session,
            };

            var result = await sessionRepository.SaveComments(comment);

            response.Success = true;
            response.Message = "Comment created successfully";
            if (result.Success) return response;
            response.Success = true;
            response.Message = "Failed to create comment";
            return response;
        }

        public async Task<IEnumerable<Session>> GetAllSession(string studentId)
        {
            return await sessionRepository.GetAllSessions(studentId);
            
        }

        public async Task<IEnumerable<Session>> GetAllSessionTutor(string tutorId)
        {
            return await sessionRepository.GetAllSessionsForTutor(tutorId);
        }

        public async Task<ApiResponse<bool>> RateSession(string sessionId, int ratings, string ratingsFor = "tutor")
        {
            var response = new ApiResponse<bool>();
            var session = await sessionRepository.FindSessionByIdAsync(sessionId);
            response.Message = "Session not found";
            if (session == null) return response;

            switch (ratingsFor)
            {
                case "student":
                    session.RateStudent = ratings;
                    break;
                default:
                    session.RateTutor = ratings;
                    break;
            }

            await sessionRepository.UpdateSession(session);
            response.Success = true;
            response.Message = "Ratings updated";
            return response;
        }

        public Task RemoveSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO sessionDto)
        {
            var res = new ApiResponse<bool>();
            var sessionRes = await sessionRepository.FindSessionByIdAsync(sessionDto.Id);

            if (sessionRes == null)
            {
                res.Success = false;
                return res;
            }

            sessionRes.EndTime = sessionDto.EndTime;
            sessionRes.Startime = sessionDto.StartTime;
            sessionRes.Status = sessionDto.Status;
            
            var _res = await sessionRepository.UpdateSession(sessionRes);

            res.Success = true;
            res.Data = _res;
            return res;
        }
    }
}