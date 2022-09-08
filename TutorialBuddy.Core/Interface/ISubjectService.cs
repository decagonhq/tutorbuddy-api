using System;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Interface
{
	public interface ISubjectService
	{
        Task<ApiResponse<PaginationModel<IEnumerable<RecommendSubjectDTO>>>> GetRecommendedSubject(int pageSize, int pageNumber);
        Task<ApiResponse<PaginationModel<IEnumerable<CategorySubjectDTO>>>> GetAllCategoriesWithSubject(int pageSize, int pageNumber);

    }
}

