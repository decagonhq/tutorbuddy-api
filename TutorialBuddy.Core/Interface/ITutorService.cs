﻿using System;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface ITutorService
	{
        
        Task<ApiResponse<string>> AddSubjectForATutor(string Id, IEnumerable<SubjectDTO> Subjects);
        Task<ApiResponse<string>> AddAvaliabilityForATutor(string Id, IEnumerable<AvailabilityDTO> availabilities);
        Task<ApiResponse<IEnumerable<FeatureTutorDTO>>> GetFeatureTutors();
        Task<ApiResponse<TutorResponseDTO>> GetATutor(string Id);
    }
}

