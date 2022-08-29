using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface ITutorService
	{
        Task<IEnumerable<Tutor>> GetFeatureTutors(int num);

    }
}

