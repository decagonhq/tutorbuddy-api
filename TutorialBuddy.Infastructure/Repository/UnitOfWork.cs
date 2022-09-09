﻿using TutorBuddy.Core.Interface;
using TutorBuddy.Infrastructure.DataAccess;

namespace TutorBuddy.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TutorBuddyContext _appDbContext;
        private IUserRepository _userRepository;
        private ITutorRepository _tutorRepository;
        private ISubjectRepository _subjectRepository;
        private IAvailabilityRepository _availabilityRepository;
        private IStudentRepository _studentRepository;
        public UnitOfWork(TutorBuddyContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_appDbContext);
        public ITutorRepository TutorRepository => _tutorRepository ??= new TutorRepository(_appDbContext);
        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_appDbContext);
        public ISubjectRepository SubjectRepository => _subjectRepository ??= new SubjectRepository(_appDbContext);
        public IAvailabilityRepository AvailabilityRepository => _availabilityRepository ??= new AvaliabilityRepository(_appDbContext);

        public void Dispose()
        {
            _appDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> Save()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}

