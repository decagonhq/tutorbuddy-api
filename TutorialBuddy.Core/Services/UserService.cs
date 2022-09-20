using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;
using Serilog;
using AutoMapper;

namespace TutorBuddy.Core.Services
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IImageUploadService _imageUpload;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IImageUploadService imageUpload, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _imageUpload = imageUpload;
            _mapper = mapper;
           

        }

        public async Task<ApiResponse<UserResponseDTO>> GetUser(string Id)
        {

            var user = await _unitOfWork.UserRepository.GetAUser(Id, "Student");
            var response = new ApiResponse<UserResponseDTO>();

            if (user != null)
            {
                var result = new UserResponseDTO()
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    AvatarUrl = user.AvatarUrl
                    
                };

                response.Data = result;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = true;
                response.Message = "User Update successfully";

                return response;

            }

            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = false;
            response.Message = "User Not Found";

            return response;

        }

       

        public async Task<ApiResponse<string>> UpdateAsync(UpdateUserDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var response = new ApiResponse<string>();
           
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                var result = await _userManager.UpdateAsync(user);
               

                if (result.Succeeded)
                {
                    response.Data = user.Id;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = true;
                    response.Message = "User Update successfully";

                    return response;
                }

                response.Data = user.Id;
                response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                response.Success = false;
                response.Message = "User Update is unsuccessfully";

                return response;
            }

            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = false;
            response.Message = "User Not Found";

            return response;
        }

        public async Task<ApiResponse<string>> UpdatePasswordAsync(UpdatePasswordDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var response = new ApiResponse<string>();

            if (user != null)
            {

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if(result.Succeeded)
                {
                    response.Data = user.Id;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Success = true;
                    response.Message = "Password change successfully";

                    return response;
                }

                response.Data = user.Id;
                response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                response.Success = false;
                response.Message = "Password change unsuccessfully";

                return response;
            }

            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Success = false;
            response.Message = "User Not Found";

            return response;
        }

        public async Task<ApiResponse<string>> UploadUserAvatarAsync(string Id, UploadImageDTO imageDto)
        {
            Log.Information("Successfull enter the image upload service");
            var user = await _userManager.FindByIdAsync(Id);
            var response = new ApiResponse<string>();

            if (user != null)
            {
                Log.Information("User is found");
                var upload = await _imageUpload.UploadSingleImage(imageDto.ImageToUpload, "Avatar");

                Log.Information("Successful upload the image");
                user.AvatarUrl = upload.Url.ToString();
                user.PublicUrl = upload.PublicId;
                await _userManager.UpdateAsync(user);

                response.Data = user.AvatarUrl;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Image uploaded successfully";

                return response;


            }

            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.BadGateway;
            response.Success = false;
            response.Message = "Image uploaded not successfully";

            return response;


        }

        public async Task<ApiResponse<IEnumerable<NotificationDTO>>> GetUserNotification(string Id)
        {
            var response = new ApiResponse<IEnumerable<NotificationDTO>>();

            var user = await _unitOfWork.UserRepository.GetAUserNotification(Id);

            if(user != null && user.Notifications != null)
            {
                List<NotificationDTO> notifications = new List<NotificationDTO>();
                foreach (var item in user.Notifications)
                {
                    if(!item.Isread)
                    {
                        var sender = await _userManager.FindByIdAsync(item.SenderId);
                        NotificationDTO notify = new NotificationDTO()
                        {
                            Id = item.ID,
                            SenderName = sender.FirstName + " " + sender.LastName,
                            SenderImage = sender.AvatarUrl,
                            Message = item.Message,
                            CreatedAt = item.CreatedOn

                        };

                        notifications.Add(notify);
                    }
                }

                response.Data = notifications;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Successfully";

                return response;
            }

            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Success = false;
            response.Message = "UnSuccessfully";

            return response;
        }

        public async Task<ApiResponse<IEnumerable<ReminderDTO>>> GetUserReminders(string Id)
        {
            var response = new ApiResponse<IEnumerable<ReminderDTO>>();

            var user = await _unitOfWork.UserRepository.GetAUserReminders(Id);
            List<ReminderDTO> result = new List<ReminderDTO>();
            if(user != null && user.Reminders != null)
            {
                var newDay = DateTime.Now;
                var reminders = user.Reminders.Where(x => x.StartTime >= newDay.AddDays(-1));
                foreach (var item in reminders)
                {
                    result.Add(_mapper.Map<ReminderDTO>(item));
                }


                response.Data = result;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Successfully";

                return response;

            }


            response.Data = null;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Success = false;
            response.Message = "UnSuccessfully";

            return response;


        }

    }
}

