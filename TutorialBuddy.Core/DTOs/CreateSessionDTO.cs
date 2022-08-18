using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.DTOs
{
    public class CreateSessionDTO
    {
        public string? TutorSubjectId { get; set; }
        public string? StudentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus SessionStatus { get; set; }
    }
}