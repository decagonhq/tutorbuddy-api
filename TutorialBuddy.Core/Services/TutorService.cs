using System;
using TutorBuddy.Core.DTOs;
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



        public IEnumerable<FeatureTutorDTO> GetFeatureTutors(int num)
        {
            var tutors = _unitOfWork.TutorRepository.GetFeatureTutors(num);

            return tutors;
        }
	}
}

