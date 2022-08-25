using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IUserRepository userRepository;
        private readonly ITutorSubjectRepository tutorSubjectRepository;

        public SessionService(ISessionRepository sessionRepository,
            IUserRepository userRepository,
            ISubjectRepository subjectRepository,
            ITutorSubjectRepository tutorSubjectRepository
            )
        {
            this.sessionRepository = sessionRepository;
            this.userRepository = userRepository;
            this.tutorSubjectRepository = tutorSubjectRepository;
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
                TutorSubject = await  tutorSubjectRepository.GetDetail(createSession.TutorSubjectId!),
                Student = student,
            };

            var res = await sessionRepository.AddSession(session, student);

            response.Success = res;
            return response;
        }

        public async Task<IEnumerable<Session>> GetAllSession(string studentId)
        {
            var result = await sessionRepository.GetAllSessions(studentId);
            return result;
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