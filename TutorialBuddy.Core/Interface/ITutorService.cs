using System;
using Microsoft.AspNetCore.Mvc;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface ITutorService
	{
        IEnumerable<FeatureTutorDTO> GetFeatureTutors(int num);
        Task<ApiResponse<string>> AddSubjectForATutor(string Id, IEnumerable<SubjectDTO> Subjects);
    }
}

