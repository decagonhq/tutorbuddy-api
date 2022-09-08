using System;
using TutorBuddy.Core.DTOs;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface ISubjectService
	{
        Task<ApiResponse<IEnumerable<RecommendSubjectDTO>>> GetRecommendedSubject(int num);

    }
}

