using System;
using System.ComponentModel.DataAnnotations;

namespace TutorBuddy.Core.Models
{
	public class TutorAvaliability : BaseEntity
	{
       
        public Tutor Tutor { get; set; }
        
        public Availability Availability { get; set; }
    }
}

