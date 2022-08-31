using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
	public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public TutorService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;

        }

        public async Task<ApiResponse<string>> AddSubjectForATutor(string Id, IEnumerable<SubjectDTO> Subjects)
        {
            var response = new ApiResponse<string>();
            //var tutor = await _userManager.FindByIdAsync(Id);
            var tutor = await _unitOfWork.TutorRepository.GetARecord(Id);

            if(tutor != null)
            {
                var subjects = _mapper.Map<IEnumerable<Subject>>(Subjects);
                await _unitOfWork.TutorRepository.AddTutorSubjects(tutor, subjects);
                await _unitOfWork.Save();
                response.Message = $"Subject(s) are added successfully!!!";
                response.Success = true;
                response.Data = string.Empty;
                response.StatusCode = (int)HttpStatusCode.Created;
                return response;
            }

            response.Message = $"No record of tutor is found on our DB";
            response.Success = false;
            response.Data = string.Empty;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
        }

        public IEnumerable<FeatureTutorDTO> GetFeatureTutors(int num)
        {
            var tutors = _unitOfWork.TutorRepository.GetFeatureTutors(num);

            return tutors;
        }
	}
}

