using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Infrastructure.DataAccess
{
    public class TutorBuddyContext : IdentityDbContext<User>
    {
        public TutorBuddyContext(DbContextOptions<TutorBuddyContext> options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.ID = Guid.NewGuid().ToString();
                        item.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        }

       
        public DbSet<Availability> Availabilities { get; set; }
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}