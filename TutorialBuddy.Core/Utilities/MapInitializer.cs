using System;
using AutoMapper;
using TutorBuddy.Core.DTOs;
using TutorBuddy.Core.Models;

namespace TutorBuddy.Core.Utilities
{
	public class MapInitializer : Profile
    {
		public MapInitializer()
		{
            //Authentication
            CreateMap<Availability, AvailabilityDTO>().ReverseMap();
            CreateMap<SubjectDTO, Subject>().ReverseMap()
                .ForMember(x => x.Subject, opt => opt.MapFrom(src => src.Topic));

            //CreateMap<Tutor, FeatureTutorDTO>().ReverseMap()
            //    .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
            //    .ForMember(x => x.User.AvatarUrl, opt => opt.MapFrom(x => x.Avatar))
            //    .ForMember(x => x.User.FirstName, opt => opt.MapFrom(x => x.Fullname))
            //    .ForMember(x => x.TutorSubjects.Select(x => x.Sessions.Select(x => x.RateTutors)), opt => opt.MapFrom(x => x.Id));

        }
	}
}

