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
        }
	}
}

