
using Microsoft.EntityFrameworkCore;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
	public class SubjectRepository :  ISubjectRepository
    {
        private readonly TutorBuddyContext _context;
        public SubjectRepository(TutorBuddyContext dbContext) 
        {
            _context = dbContext;
        }


        public async Task<IEnumerable<Subject>> GetAllSubjectAsync()
        {
            var subject = await _context.Subjects
                          .ToListAsync();
            return subject;
        }

        public async Task<Subject> GetASubjectAsync(string subjectId)
        {
            var subject = await _context.Subjects
                         .Where(x => x.ID == subjectId)
                         .FirstOrDefaultAsync();
            return subject;
            
        }

        public async Task<IEnumerable<Subject>> GetAllRecommendSubjectAsync()
        {
            var subject = await _context.Subjects
                          .Include(x => x.TutorSubjects)
                            .ThenInclude(x => x.Sessions)
                          .ToListAsync();

            return subject;
        }

        public async Task<IEnumerable<Category>> GetAllSubjectsWithCategoryAsync()
        {
            var subject = await _context.Categories
                          .Include(x => x.Subjects)
                          .ToListAsync();

            return subject;
        }


    }
}

