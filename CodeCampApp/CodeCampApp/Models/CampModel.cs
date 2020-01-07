using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCampApp.Models
{
    public class CampModel
    {
       
        public string Name { get; set; }
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        public int Length { get; set; }

        // Child model using not nested relationship
        // On purpose named "Venue" with no "Location" to provide
        // special configuration in AutoMapper profile to connect an entity to 
        // this property
        public string Venue { get; set; }
        // If properties that belong to a child entity are named as the entity itself
        // for example "Locaiton", AutoMapper automatically will know what to do.
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationAddress3 { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }

        // Child model using nested relationship
        // Also a Model can have a regulr definition for a child model class
        public ICollection<TalkModel> Talks { get; set; }

    }
}
