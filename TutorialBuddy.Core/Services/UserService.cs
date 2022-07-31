using System;
using TutorBuddy.Core.Interface;

namespace TutorBuddy.Core.Services
{
	public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}

