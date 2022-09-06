﻿using System;
using System.Net;
using System.Xml.Linq;
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


        public async Task<ApiResponse<string>> AddAvaliabilityForATutor(string Id, IEnumerable<AvailabilityDTO> availabilities)
        {
            var response = new ApiResponse<string>();
            
            var tutor = await _unitOfWork.TutorRepository.GetARecord(Id);

            if (tutor != null)
            {
                var avals = _mapper.Map<IEnumerable<Availability>>(availabilities);
                await _unitOfWork.TutorRepository.AddTutorAvailability(tutor, avals);
                await _unitOfWork.Save();
                response.Message = $"Day(s) are added successfully!!!";
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

        public async Task<ApiResponse<IEnumerable<FeatureTutorDTO>>> GetFeatureTutors(int num)
        {
            var response = new ApiResponse<IEnumerable<FeatureTutorDTO>>();
            var tutors = await _unitOfWork.TutorRepository.GetFeatureTutors(num);
            if(tutors != null)
            {
                response.Message = "successfully!!!";
                response.Success = true;
                response.Data = tutors;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            response.Message = $"No record of tutor is found on our DB";
            response.Success = false;
            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
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
                    subj.Subject = item;
                    foreach (var element in tutorSubj)
                    {
                        var tutor = await _userManager.FindByIdAsync(element.ID);
                        subj.Tutor = tutor.FirstName + " " + tutor.LastName;
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

