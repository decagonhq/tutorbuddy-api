using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface ISubjectRepository
	{
        Task<IEnumerable<Subject>> GetAllSubjectAsync();
        Task<Subject> GetASubjectAsync(string subjectId);
    }
}

