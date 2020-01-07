using AutoMapper;
using CodeCamp.Domain.Entities;
using CodeCampApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCampApp.Mappings
{
    public class MappingProfile : Profile
    {
        // Constructors ..............................
        public MappingProfile()
        {
            // Camp to CampModel mapping
            //this.CreateMap<Camp, CampModel>()
            //    .ReverseMap();

            // Camp DTO to CampModel 
            // ForMember -> Model -> Entity, 
            // but defining explicit mapping from Camp.Venue -> CampModel.Location.VenueName
            // ReverseMap: To indicate bidirectional mapping (Model <--> Entitiy)
            this.CreateMap<Camp, CampModel>()
               .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
               .ReverseMap();


            // Example for a mapping ignoring to populate Parent and Children objects
            this.CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore())
                .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, SpeakerModel>()
               .ReverseMap();

            // < Create other mappinsg in here >
            // Add as many of these lines as you need to map your objects
            // CreateMap<User, UserDto>();
            // CreateMap<UserDto, User>();
        }
    }
}
