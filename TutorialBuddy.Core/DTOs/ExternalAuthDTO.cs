﻿using System;
namespace TutorBuddy.Core.DTOs
{
	public class ExternalAuthDTO
	{
        public string? Provider { get; set; }
        public string? IdToken { get; set; }
    }
}

