using System;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
	public interface IAvailabilityRepository
	{
        Task<IEnumerable<Availability>> GetAllAvaliabilityAsync();
        IEnumerable<Availability> GetATutorAvaliabilityAsync(string tutorId);

    }
}

