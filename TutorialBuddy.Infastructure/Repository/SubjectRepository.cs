
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

        public async Task<IEnumerable<IGrouping<Category,Subject>>> GetAllSubjectsWithCategoryAsync()
        {
            var subject = await _context.Subjects
                          .Include(x => x.Category)
                          .Include(x => x.TutorSubjects)
                            .ThenInclude(x => x.Sessions)
                          
                          .ToListAsync();
            var result  = subject.GroupBy(x => x.Category);
            return result;
        }

        public async Task<TutorSubject> GetASubjectDetialAsync(string tutorSubjectId)
        {
            var subject = await _context.TutorSubjects
                         .Where(x => x.ID == tutorSubjectId)
                         .Include(x => x.Sessions)
                         .FirstOrDefaultAsync();
            return subject;

        }
    }
}

