using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Interface
{
    public interface ITutorSubjectRepository
    {
        Task<TutorSubject> GetDetail(string Id);
        Task<IEnumerable<TutorSubject>> GetAllSubjectATutor(string Id);
    }
}
