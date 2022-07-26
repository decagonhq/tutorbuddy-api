using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorialBuddy.Core.Models;

namespace TutorialBuddy.Infastructure
{
    public class TutorBuddyContext : IdentityDbContext<User>
    {
        public TutorBuddyContext(DbContextOptions<TutorBuddyContext> options) : base(options)
        {
        }

        public DbSet<AreaOfInterest> AreaOfInterests { get; set; }
        public DbSet<Avialability> Avialabilities { get; set; }
        public DbSet<RateStudent> RateStudents { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<RateTutor> RateTutors { get; set; }
        public DbSet<Tutor> Tutor { get; set; }
        public DbSet<TutorSubjects> TutorSubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentComment> StudentComments { get; set; }
        public DbSet<TutorComment> TutorComments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ImageMeta> ImageMeta { get; set; }
    }
}