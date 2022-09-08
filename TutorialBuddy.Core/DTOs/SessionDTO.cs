﻿using System;
using TutorialBuddy.Core.Enums;

namespace TutorBuddy.Core.DTOs
{
	public class StudentSessionDTO
	{
        public string? ID { get; set; }
        public int RateStudent { get; set; }
        public string? StudentComment { get; set; }
        public DateTime Startime { get; set; }
        public DateTime EndTime { get; set; }
        public SessionStatus Status { get; set; }
    }
}

