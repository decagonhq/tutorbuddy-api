using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface ISubjectRepository
	{
        Task<IEnumerable<Subject>> GetAllSubjectAsync();

    }
}

