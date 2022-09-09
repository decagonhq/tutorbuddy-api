using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using System.Linq;
using System.Net;
using System.Security.Claims;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Core.Utilities;
using TutorialBuddy.Core;
using TutorialBuddy.Core.Enums;
using static Google.Apis.Requests.BatchRequest;

namespace TutorBuddy.Core.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IUserRepository userRepository;
        private readonly ITutorSubjectRepository tutorSubjectRepository;
        private readonly UserManager<User> userManager;
        private readonly IAvailabilityRepository availability;
        private readonly ISubjectRepository subjectRepository;
        private readonly IMapper _mapper;
        public SessionService(ISessionRepository sessionRepository,
            IUserRepository userRepository,
            ISubjectRepository subjectRepository,
            ITutorSubjectRepository tutorSubjectRepository,
            UserManager<User> userManager, IAvailabilityRepository availability, IMapper mapper
            )
        {
            this.sessionRepository = sessionRepository;
            this.userRepository = userRepository;
            this.tutorSubjectRepository = tutorSubjectRepository;
            this.userManager = userManager;
            this.availability = availability;
            this.subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> AddSession(CreateSessionDTO createSession)
        {
            var response = new ApiResponse<bool>();
            var student = await userManager.FindByIdAsync(createSession.StudentId);
            var roles = await userManager.GetRolesAsync(student);
            if (!roles.Contains(UserRole.Student.ToString()))
            {
                response.Message = "Only user with student role can make request";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            // Check if the request session falls in the avaliabilty of the Tutor
            var TutorSubject = await tutorSubjectRepository.GetDetail(createSession.TutorSubjectId!);
            var avaliabities = await availability.GetATutorAvaliabilityAsync(TutorSubject.TutorID);

            if (!avaliabities.Exists(x => x.Day == createSession.StartTime.DayOfWeek.ToString()))
            {
                response.Message = $"Tutor is not avaliable on {createSession.StartTime.DayOfWeek.ToString()}";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Success = false;
                return response;
            }

            var session = new Session
            {
                CreatedOn = DateTime.Now,
                Startime = createSession.StartTime,
                EndTime = createSession.EndTime,
                Status = TutorialBuddy.Core.Enums.SessionStatus.requested,
                TutorSubject = TutorSubject,
                Student = student,
            };

            await sessionRepository.AddSession(session);

            response.Message = "Session added successfully!!!";
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Success = true;
            return response;
        }

        public async Task<ApiResponse<bool>> CommentOnSession(string sessionId, CreateCommentDTO createComment, string commentFor)
        {
            var response = new ApiResponse<bool>();
            var session = await sessionRepository.FindSessionByIdAsync(sessionId);


            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Message = "The session does not exit";
            if (session == null)
                return response;

            if (session.Status != SessionStatus.completed)
            {
                response.Message = "You can't comment at this point";
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                return response;
            }


            if(commentFor == UserRole.Student.ToString())
            {
                session.StudentComment = createComment.Comment;
            }
            else
            {
                session.TutorComment = createComment.Comment;
            }


            var result = await sessionRepository.UpdateSession(session);
            response.Success = true;
            response.StatusCode = (int)HttpStatusCode.Created;
            response.Message = "Comment created successfully";
            if (result) return response;
            response.Success = false;
            response.StatusCode = (int)HttpStatusCode.NotModified;
            response.Message = "Failed to create comment";
            return response;
        }

        

        public async Task<ApiResponse<PaginationModel<IEnumerable<StudentSessionResponseDTO>>>> GetAllSessionForStudent(string studentId, int pageSize, int pageNumber)
        {
            var response = new ApiResponse<PaginationModel<IEnumerable<StudentSessionResponseDTO>>>();
            var studentSession = await sessionRepository.GetAllSessionsForAstudent(studentId);
            List<StudentSessionResponseDTO> result = new List<StudentSessionResponseDTO>();

            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Message = "The session does not exit";
            if (studentSession == null)
                return response;

            var tutorSubject = studentSession.Sessions.Select(x => x.TutorSubject);
            

            foreach (var item in tutorSubject)
            {
                if(item.Sessions.Any())
                {
                    var tutor = await userManager.FindByIdAsync(item.TutorID);
                    var subject = await subjectRepository.GetASubjectAsync(item.SubjectID);
                    foreach (var ele in item.Sessions)
                    {
                        StudentSessionResponseDTO sess = new StudentSessionResponseDTO()
                        {
                            SessionId = ele.ID,
                            Topic = subject.Topic,
                            Thumbnail = subject.Thumbnail,
                            Tutor = tutor.FirstName + " " + tutor.LastName,
                            TutorImage = tutor.AvatarUrl,
                            Startime = ele.Startime,
                            EndTime = ele.EndTime,
                            Status = ele.Status
                        };

                        result.Add(sess);
                    }
                    
                }

               
            }

            var paginatedResult = Pagination.PaginationAsync(result, pageSize, pageNumber);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = paginatedResult;
            response.Success = true;
            response.Message = "Get session Successfully";
            return response;

        }

        public async Task<ApiResponse<PaginationModel<IEnumerable<TutorSessionResponseDTO>>>> GetAllSessionForTutor(string tutorId, int pageSize, int pageNumber)
        {
            var response = new ApiResponse<PaginationModel<IEnumerable<TutorSessionResponseDTO>>>();
            var tutorSession = await sessionRepository.GetAllSessionsForTutor(tutorId);
            List<TutorSessionResponseDTO> result = new List<TutorSessionResponseDTO>();

            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Message = "The session does not exit";
            if (tutorSession == null)
                return response;



            var tutorSubject = tutorSession.TutorSubjects;
            foreach (var item in tutorSubject)
            {
                if (item.Sessions.Any())
                {
                    var subject = await subjectRepository.GetASubjectAsync(item.SubjectID);

                    foreach (var ele in item.Sessions)
                    {
                        TutorSessionResponseDTO sess = new TutorSessionResponseDTO()
                        {
                            SessionId = ele.ID,
                            Topic = subject.Topic,
                            Thumbnail = subject.Thumbnail,
                            Student = ele.Student.FirstName + " " + ele.Student.LastName,
                            StudentImage = ele.Student.AvatarUrl,
                            Startime = ele.Startime,
                            EndTime = ele.EndTime,
                            Status = ele.Status
                        };

                        result.Add(sess);
                    }

                    
                }

            }



            var paginatedResult = Pagination.PaginationAsync(result, pageSize, pageNumber);
            response.Success = true;
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Data = paginatedResult;
            response.Message = "Get session Successfully";
            return response;
        }

        public async Task<ApiResponse<bool>> RateSession(string sessionId, int ratings, string ratingsFor)
        {
            var response = new ApiResponse<bool>();
            var session = await sessionRepository.FindSessionByIdAsync(sessionId);
            response.Message = "Session not found";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = false;
            if (session == null) return response;

            if(session.Status != SessionStatus.completed)
            {
                response.Message = "You can only rate session that has been completed";
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                response.Success = false;
                return response;
            }
            switch (ratingsFor)
            {
                case "Student":
                    session.RateStudent = ratings;
                    break;
                default:
                    session.RateTutor = ratings;
                    break;
            }

            await sessionRepository.UpdateSession(session);
            response.Success = true;
            response.Message = "Ratings updated";
            response.StatusCode = (int)HttpStatusCode.OK;
            return response;
        }

        public Task RemoveSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> UpdateSession(UpdateSessionDTO sessionDto, string Id)
        {
            var res = new ApiResponse<bool>();
            var sessionRes = await sessionRepository.FindSessionByIdAsync(Id);

            if (sessionRes == null)
            {
                res.Success = false;
                res.StatusCode = (int)HttpStatusCode.NotFound;
                res.Message = "The session does not exit";
                return res;
            }

           
            sessionRes.Status = sessionDto.Status;
            
            var _res = await sessionRepository.UpdateSession(sessionRes);

            res.Success = true;
            res.Data = _res;
            res.StatusCode = (int)HttpStatusCode.Created;
            res.Message = "Session status updated successfully";
            return res;
        }
    }
}