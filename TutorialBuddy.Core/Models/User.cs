﻿using Microsoft.AspNetCore.Identity;

namespace TutorBuddy.Core.Models
{
    public class User : IdentityUser
    {

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? RefreshToken { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public IEnumerable<Tutor> Tutors { get; set; }
        public IEnumerable<Reminder> Reminders { get; set; }
        public IEnumerable<AreaOfInterest> AreaOfInterests { get; set;}

    }
}
