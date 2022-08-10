using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorialBuddy.Core;

namespace TutorBuddy.Core.Services
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IImageUploadService _imageUpload;
        public UserService(IUnitOfWork unitOfWork, IImageUploadService imageUpload, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _imageUpload = imageUpload;

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
            var user = await _userManager.FindByIdAsync(Id);
            var response = new ApiResponse<string>();

            if (user != null)
            {
                var upload = await _imageUpload.UploadSingleImage(imageDto.ImageToUpload, "Avatar");


                user.AvatarUrl = upload.Url.ToString();
                user.PublicUrl = upload.PublicId;
                await _userManager.UpdateAsync(user);

                response.Data = user.Id;
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
    }
}

