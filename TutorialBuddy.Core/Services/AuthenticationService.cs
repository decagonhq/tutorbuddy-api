using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Transactions;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Enums;
using TutorBuddy.Core.Interface;
using TutorBuddy.Core.Models;
using TutorBuddy.Core.Utilities;
using TutorialBuddy.Core;
using TutorialBuddy.Core.Enums;
using TutorialBuddy.Infastructure.NotificationProviders;

namespace TutorBuddy.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        // COPIED
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IMapper _mapper;

        public AuthenticationService(IServiceProvider provider)
        {
            _userManager = provider.GetRequiredService<UserManager<User>>();
            _roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            _tokenGenerator = provider.GetRequiredService<ITokenGeneratorService>();
            _notificationService = provider.GetRequiredService<INotificationService>();
            _unitOfWork = provider.GetRequiredService<IUnitOfWork>();
            _logger = provider.GetRequiredService<ILogger<AuthenticationService>>();
            _mapper = provider.GetRequiredService<IMapper>();
        }

        //public async Task<ApiResponse<string>> AddUser(RegisterDTO model)
        //{
        //    var response = new ApiResponse<string>();


        //    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //    {
        //        var (emailResponse, registerResponse) = await Register(model);
        //        if (registerResponse.Success)
        //        {
        //            if (emailResponse)
        //            {
        //                _logger.LogInformation("Mail sent successfully");
        //                response.StatusCode = (int)HttpStatusCode.Created;
        //                response.Success = true;
        //                response.Data = registerResponse.Data.Id;
        //                response.Message = "User created successfully! Please check your mail to verify your account.";
        //                transaction.Complete();
        //                return response;
        //            }
        //            _logger.LogInformation("Mail service failed");
        //            transaction.Dispose();
        //            response.StatusCode = (int)HttpStatusCode.BadRequest;
        //            response.Success = false;
        //            response.Message = "Registration failed. Please try again";
        //            return response;
        //        }

        //        response.StatusCode = registerResponse.StatusCode;
        //        response.Success = registerResponse.Success;
        //        response.Data = string.Empty;
        //        response.Message = registerResponse.Message;
        //        transaction.Complete();
        //        return response;
        //    }
        //}

        public async Task<ApiResponse<GetRegisterResponseDTO>> GetRegisterResource()
        {
            var response = new ApiResponse<GetRegisterResponseDTO>();
            var roles = _roleManager.Roles;
            var subjects = await _unitOfWork.SubjectRepository.GetAllSubjectAsync();
            var avaliabilities = await _unitOfWork.AvailabilityRepository.GetAllAvaliabilityAsync();

            var result = new GetRegisterResponseDTO()
            {
                Roles = roles.Select(x => x.Name),
                Avaliabilities = _mapper.Map<IEnumerable<AvailabilityDTO>>(avaliabilities),
                Subjects = _mapper.Map<IEnumerable<SubjectDTO>>(subjects)
                
            };

            response.StatusCode = (int)HttpStatusCode.OK;
            response.Success = true;
            response.Data = result;
            response.Message = "successfully!";

            return response;

        }


        public async Task<ApiResponse<string>> AddTutor(RegisterDTO model)
        {
            var response = new ApiResponse<string>();
            
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var (emailResponse, registerResponse) = await Register(model);
                if (registerResponse.Success)
                {
                    if (emailResponse)
                    {
                        _logger.LogInformation("Mail sent successfully");

                       
                        var tutor = new Tutor()
                        {
                            BioNote = model.Bio,
                            User = registerResponse.Data,
                            Price = model.Price,
                            UnitOfPrice = model.UnitOfPrice
                            
                        };
                        //await _unitOfWork.TutorRepository.Add(tutor);
                       
                        //await _unitOfWork.TutorRepository.AddTutorSubjects(tutor, subjects);
                        //var availabilities = new List<Availability>();
                        //availabilities.AddRange(addTutorDTO.Availability.Select(a => new Availability()
                        //{
                        //    Day = a.Day
                        //}));
                        // await _unitOfWork.TutorRepository.AddTutorAvailability(tutor, availabilities);
                        //await _unitOfWork.Save();
                        response.StatusCode = (int)HttpStatusCode.Created;
                        response.Success = true;
                        response.Data = registerResponse.Data.Id;
                        response.Message = "User created successfully! Please check your mail to verify your account.";
                        transaction.Complete();
                        return response;
                    }
                    _logger.LogInformation("Mail service failed");
                    transaction.Dispose();
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Success = false;
                    response.Message = "Registration failed. Please try again";
                    return response;
                }

                response.StatusCode = registerResponse.StatusCode;
                response.Success = registerResponse.Success;
                response.Data = string.Empty;
                response.Message = registerResponse.Message;
                transaction.Complete();
                return response;
            }
        }



        private async Task<(bool, ApiResponse<User>)> Register(RegisterDTO baseRegister)
        {
            var user = new User()
            {
                FirstName = baseRegister.FirstName,
                LastName = baseRegister.LastName,
                Email = baseRegister.Email,
                UserName = $"{baseRegister.FirstName[0]}{baseRegister.LastName[0]}{new Random().Next(1001,10000)}"
            };
            var response = new ApiResponse<User>();

            var result = await _userManager.CreateAsync(user, baseRegister.Password);
            if (result.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(user, baseRegister.Role);
                if(!addRoleResult.Succeeded)
                {
                    response.Message = GetErrors(result);
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Success = false;
                    return (false, response);
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = TokenConverter.EncodeToken(token);
                var userRoles = await _userManager.GetRolesAsync(user);
                    
                var mailBody = await EmailBodyBuilder.GetEmailBody(user, userRoles.ToList(), emailTempPath: "StaticFiles/HTML/ConfirmEmail.html", linkName: "ConfirmEmail", encodedToken, controllerName: "Auth");
                NotificationContext notificationContext = new NotificationContext()
                {
                    Address = baseRegister.Email,
                    Header = "Confirm Your Registration",
                    Payload = mailBody
                };

                response.Data = user;
                response.Success = true;
                return (await _notificationService.SendAsync(NotifyWith.Email, notificationContext), response);
            }

            response.Message = GetErrors(result);
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Success = false;
            return (false, response);
        }

        public async Task<ApiResponse<string>> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDTO.EmailAddress);
            var response = new ApiResponse<string>();
            if (user == null)
            {
                response.Message = "User not found";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }
            var decodedToken = TokenConverter.DecodeToken(confirmEmailDTO.Token);
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Email Confirmation successful";
                response.Data = user.Id;
                response.Success = true;
                return response;
            }
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Message = GetErrors(result);
            response.Data = string.Empty;
            response.Success = false;
            return response;
        }

        public async Task<ApiResponse<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = new ApiResponse<string>();

            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.EmailAddress);
            if (user == null)
            {
                response.Message = $"An email has been sent to {forgotPasswordDTO.EmailAddress} if it exists";
                response.Success = true;
                response.Data = string.Empty;
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = TokenConverter.EncodeToken(token);
            var userRole = await _userManager.GetRolesAsync(user);

            var mailBody = await EmailBodyBuilder.GetEmailBody(user, userRole.ToList(), emailTempPath: "StaticFiles/HTML/ForgotPassword.html", linkName: "ResetPassword", encodedToken, controllerName: "Auth");

            NotificationContext notificationContext = new NotificationContext()
            {
                Address = forgotPasswordDTO.EmailAddress,
                Header = "Reset Password Request",
                Payload = mailBody
            };

            var emailResult = await _notificationService.SendAsync(NotifyWith.Email, notificationContext);
            if (emailResult)
            {
                response.Success = true;
                response.Message = $"An email has been sent to {forgotPasswordDTO.EmailAddress} if it exists";
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = forgotPasswordDTO.EmailAddress;
                return response;
            }

            response.Success = false;
            response.Message = "Something went wrong. Please try again.";
            response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            return response;
        }

        public async Task<ApiResponse<CredentialResponseDTO>> LoginUser(LoginUserDTO loginUserDTO)
        {
            _logger.LogInformation("Login Attempt");
            var validityResult = await ValidateUser(loginUserDTO);
            var response = new ApiResponse<CredentialResponseDTO>();

            if (!validityResult.Success)
            {
                _logger.LogError("Login operation failed");
                response.Message = validityResult.Message;
                response.StatusCode = validityResult.StatusCode;
                response.Success = false;
                return response;
            }

            var user = await _userManager.FindByEmailAsync(loginUserDTO.EmailAddress);
            user.RefreshToken = _tokenGenerator.GenerateRefreshToken().ToString();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); //sets refresh token for 7 days

            var credentialResponse = new CredentialResponseDTO()
            {
                Id = user.Id,
                Token = await _tokenGenerator.GenerateToken(user),
                RefreshToken = user.RefreshToken
            };

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                _logger.LogInformation("User successfully logged in");
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "Login Successfully";
                response.Data = credentialResponse;
                response.Success = true;
                return response;
            }
            
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = GetErrors(result);
            response.Success = false;
            return response;
        }

        public async Task<ApiResponse<string>> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var response = new ApiResponse<string>();
            _logger.LogInformation("Reset password attempt");
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.EmailAddress);

            if (user == null)
            {
                response.Message = "Invalid user!";
                response.Success = false;
                response.Data = string.Empty;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                return response;
            }

            var decodedToken = TokenConverter.DecodeToken(resetPasswordDTO.Token);

            var purpose = UserManager<User>.ResetPasswordTokenPurpose;
            var tokenProvider = _userManager.Options.Tokens.PasswordResetTokenProvider;

            var isValidToken = await _userManager.VerifyUserTokenAsync(user, tokenProvider, purpose, decodedToken);

            var result = new IdentityResult();
            if (isValidToken)
                result = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDTO.NewPassword);

            if(result.Succeeded)
            {
                response.Success = true;
                response.Data = user.Id;
                response.Message = "Password has been reset successfully";
                response.StatusCode = (int)HttpStatusCode.OK;
                return response;
            }

            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Data = string.Empty;
            response.Message = GetErrors(result);
            response.Success = false;
            return response;
        }

        private static string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }

        private async Task<ApiResponse<bool>> ValidateUser(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.EmailAddress);
            var response = new ApiResponse<bool>();
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                response.Message = "Invalid Credentials";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                return response;
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                response.Message = "Account not activated";
                response.Success = false;
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                return response;
            }
            response.Success = true;
            return response;
        }
    }
}
