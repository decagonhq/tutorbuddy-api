using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
	public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ISubjectRepository _subject;
        private readonly IMapper _mapper;
        public SubjectService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
           

        }


        public async Task<ApiResponse<IEnumerable<RecommendSubjectDTO>>> GetRecommendedSubject(int num)
        {
            var response = new ApiResponse<IEnumerable<RecommendSubjectDTO>>();
            var subjects = await _unitOfWork.SubjectRepository.GetAllRecommendSubjectAsync();
            

            if (subjects != null)
            {
                List<RecommendSubjectDTO> result = new List<RecommendSubjectDTO>();
                foreach (var item in subjects)
                {
                    RecommendSubjectDTO subj = new RecommendSubjectDTO();
                    var tutorSubj = item.TutorSubjects;
                    subj.Subject = _mapper.Map<SubjectRecommedDTO>(item);
                    foreach (var element in tutorSubj)
                    {
                        var tutor = await _userManager.FindByIdAsync(element.TutorID);
                        subj.Tutor = tutor.FirstName + " " + tutor.LastName;
                        subj.TutorSubjectId = element.ID;
                        if (element.Sessions.Count() > 0)
                        {
                            int rateSum = element.Sessions.Sum(x => x.RateTutor);
                            subj.UserCount = element.Sessions.Count();
                            double calrate = rateSum / subj.UserCount;
                            subj.Rate = (int)Math.Round(calrate, 1);
                        }

                    }

                    result.Add(subj);
                }


                response.Message = "successfully!!!";
                response.Success = true;
                response.Data = result;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            response.Message = $"No record of tutor is found on our DB";
            response.Success = false;
            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
        }
    }
}

