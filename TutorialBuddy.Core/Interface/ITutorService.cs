using System;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface ITutorService
	{
        IEnumerable<FeatureTutorDTO> GetFeatureTutors(int num);

    }
}

