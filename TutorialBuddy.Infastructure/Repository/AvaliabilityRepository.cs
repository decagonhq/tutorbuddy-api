using System;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
	public class AvaliabilityRepository : GenericRepository<Availability>, IAvailabilityRepository
    {
        private readonly TutorBuddyContext _context;
        public AvaliabilityRepository(TutorBuddyContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }


        public async Task<IEnumerable<Availability>> GetAllAvaliabilityAsync()
        {
            var availabilities = await GetAllRecord();

            return availabilities;
        }
    }
}

