using System;
using Microsoft.EntityFrameworkCore;
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


        public async Task<List<Availability>> GetATutorAvaliabilityAsync(string tutorId)
        {
            var tutorAval = _context.TutorAvaliabilities
                                 .Where(x => x.TutorID == tutorId)
                                 .ToList();

            List<Availability> results = new List<Availability>();

            foreach (var item in tutorAval)
            {
                results.Add(await GetARecord(item.AvailabilityID));
            }

            return results;
        }
    }
}

