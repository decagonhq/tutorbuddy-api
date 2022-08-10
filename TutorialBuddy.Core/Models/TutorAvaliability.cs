using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutorBuddy.Core.Models
{
	public class TutorAvaliability 
	{
        
        public string AvailabilityID { get; set; }
        public string TutorID { get; set; }
        
    }
}

