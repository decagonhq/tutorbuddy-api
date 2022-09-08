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
            CreateMap<AvailabilityDTO, Availability>().ReverseMap()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => ConvertDayToKey(src.Day))); 
            CreateMap<SubjectDTO, Subject>().ReverseMap()
                .ForMember(x => x.Subject, opt => opt.MapFrom(src => src.Topic));
            CreateMap<SubjectRecommedDTO, Subject>().ReverseMap()
                 .ForMember(x => x.Subject, opt => opt.MapFrom(src => src.Topic));
                 
            CreateMap<StudentSessionDTO, Session>().ReverseMap();
            CreateMap<TutorSessionDTO, Session>().ReverseMap();

        }

        private int ConvertDayToKey(string day)
        {
            switch (day)
            {
                case "Monday":
                    return 0;

                case "Tuesday":
                    return 1;

                case "Wednesday":
                    return 2;

                case "Thursday":
                    return 3;

                default:
                    return 4;

            }
        }
    }
}

