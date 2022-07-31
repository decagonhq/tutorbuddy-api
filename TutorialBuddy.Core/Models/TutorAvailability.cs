namespace TutorBuddy.Core.Models
{
    public class TutorAvailability : BaseEntity
    {
        public IEnumerable<Availability> Availabilities { get; set; }
        public Tutor Tutor { get; set; }
    }
}
