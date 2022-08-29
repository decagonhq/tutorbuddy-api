using System;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Services
{
	public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TutorService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }



        public async Task<IEnumerable<Tutor>> GetFeatureTutors(int num)
        {
            var tutors = await _unitOfWork.TutorRepository.GetFeatureTutors(num);

            return tutors;
        }
	}
}

