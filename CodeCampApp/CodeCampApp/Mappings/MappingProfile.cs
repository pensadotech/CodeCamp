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
            //  Mapping Domian DTO to Models in the applicaiton add a security layer
            // to protect a dirrect connection to the database. The mapping tool will
            // help sql injection and additional transformations to secure incomng data.
            // However, this also helps to simplify teh manipulation of data clusters in 
            // the frontend, specially when rendering data from more than one Domain DTO.

            // Camp to CampModel mapping
            //this.CreateMap<Camp, CampModel>()
            //    .ReverseMap();

            // Map Domain.Camp DTO to App.CampModel 
            // In this example, an explicit mapping is defined to match name differences  
            // from Comian.Camp.Venue -> App.CampModel.Location.VenueName
            // ReverseMap: is to indicate bidirectional mapping (Model <--> Entitiy)
            this.CreateMap<Camp, CampModel>()
               .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
               .ReverseMap();

            // Map Domain.Location to App.LocationModel
            // ReverseMap: is to indicate bidirectional mapping (Model <--> Entitiy)
            this.CreateMap<Location, LocationModel>()
              .ReverseMap();

            // Map Domain.Talk to App.TalkModel
            // Example for a mapping ignoring to populate Parent and Children objects
            // In this case, the Domain.Talk.Camp will not be copied. 
            // Commented out there is a second example to not bring the speaker data
            // ReverseMap: is to indicate bidirectional mapping (Model <--> Entitiy)
            this.CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore());
             // .ForMember(t => t.Speaker, opt => opt.Ignore());

            // Mapp Domain.Speaker to App.SpeakerModel
            // ReverseMap: is to indicate bidirectional mapping (Model <--> Entitiy)
            this.CreateMap<Speaker, SpeakerModel>()
               .ReverseMap();


            // < Create other mappinsg in here >
            // Add as many of these lines as you need to map your objects, for example
            // CreateMap<User, UserDto>();
            // CreateMap<UserDto, User>();
        }
    }
}
