using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Core.Utilities;
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


        public async Task<ApiResponse<PaginationModel<IEnumerable<RecommendSubjectDTO>>>> GetRecommendedSubject(int pageSize, int pageNumber)
        {
            var response = new ApiResponse<PaginationModel<IEnumerable<RecommendSubjectDTO>>>();
            var subjects = await _unitOfWork.SubjectRepository.GetAllRecommendSubjectAsync();
            

            if (subjects != null)
            {
                var result = await RefineSubject(subjects);

                var paginatedResult = Pagination.PaginationAsync(result, pageSize, pageNumber);
                response.Message = "successfully!!!";
                response.Success = true;
                response.Data = paginatedResult;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            response.Message = $"No record of tutor is found on our DB";
            response.Success = false;
            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
        }

        public async Task<ApiResponse<PaginationModel<IEnumerable<CategorySubjectDTO>>>> GetAllCategoriesWithSubject(int pageSize, int pageNumber)
        {
            var response = new ApiResponse<PaginationModel<IEnumerable<CategorySubjectDTO>>>();
            var categories = await _unitOfWork.SubjectRepository.GetAllSubjectsWithCategoryAsync();


            if (categories != null)
            {
                List<CategorySubjectDTO> result = new List<CategorySubjectDTO>();
                foreach (var item in categories)
                {
                    CategorySubjectDTO catg = new CategorySubjectDTO();
                    catg.CategoryId = item.Key.ID;
                    catg.CategoryName = item.Key.Title;
                    List<RecommendSubjectDTO> catSubjects = new List<RecommendSubjectDTO>();
                    catg.Subject = (item.Key.Subjects != null) ? await RefineSubject(item.Key.Subjects) : null;
                    result.Add(catg);
                }

                var paginatedResult = Pagination.PaginationAsync(result, pageSize, pageNumber);
                response.Message = "successfully!!!";
                response.Success = true;
                response.Data = paginatedResult;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            response.Message = $"No record of tutor is found on our DB";
            response.Success = false;
            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response;
        }


        private async Task<List<RecommendSubjectDTO>> RefineSubject(IEnumerable<Subject> subjects)
        {

            List<RecommendSubjectDTO> result = new List<RecommendSubjectDTO>();
            foreach (var item in subjects)
            {
                RecommendSubjectDTO subj = new RecommendSubjectDTO();
                var tutorSubj = item.TutorSubjects;
                if(tutorSubj != null)
                {
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
            }

            return result;

        }



        public async Task<ApiResponse<SubjectDetailDTO>> GetASubjectDetails(string tutorSubjectId)
        {
            var response = new ApiResponse<SubjectDetailDTO>();
            var tutorSubject = await _unitOfWork.SubjectRepository.GetASubjectDetialAsync(tutorSubjectId);
            var result = new SubjectDetailDTO();
            if (tutorSubject != null)
            {
                var tutor = await _unitOfWork.TutorRepository.GetTutor(tutorSubject.TutorID);
                result.Name = tutor.User.FirstName + " " + tutor.User.LastName;
                result.BioNote = tutor.BioNote;
                result.Price = tutor.Price;
                result.UnitOfPrice = tutor.UnitOfPrice;
                var subject = await _unitOfWork.SubjectRepository.GetASubjectAsync(tutorSubject.SubjectID);
                result.Topic = subject.Topic;
                result.Description = subject.Description;
                result.Thumbnail = subject.Thumbnail;
                result.CreatedAt = subject.CreatedOn;
                result.NoOfCourses = tutor.TutorSubjects.Count();
                result.Rating = tutorSubject.Sessions.Sum(x => x.RateTutor) / tutorSubject.Sessions.Count();
                result.TutorComments = tutorSubject.Sessions.Select(x => x.TutorComment).ToList();

                response.Data = result;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = true;
                return response;
            }

            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = true;
            return response;
        }

    }
}

