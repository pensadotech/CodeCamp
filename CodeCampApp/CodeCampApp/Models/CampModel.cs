using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCampApp.Models
{
    public class CampModel
    {
        public int Id { get; set; }

        // CampCode is a moniker is an alterntive ID to be used from website, instead of using the ID
        [Required, StringLength(10)]
        public string CampCode { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public int LengthInWeeks { get; set; }

        // Child model using not nested relationship
        // On purpose named "Venue" with no "Location" to provide
        // special configuration in AutoMapper profile to connect an entity to 
        // this property
        public string Venue { get; set; }
        // If properties that belong to a child entity are named as the entity itself
        // for example "Location.Address1 => LocationAddress1", AutoMapper automatically will know what to do.
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationCityTown { get; set; }
        public string LocationStateProvince { get; set; }
        public string LocationPostalCode { get; set; }
        public string LocationCountry { get; set; }
        public string LocationContactPhone { get; set; }
        // Image profile
        public string LocationProfileImageFilename { get; set; }

        // Child model using nested relationship
        // Also a Model can have a regulr definition for a child model class
        public ICollection<TalkModel> Talks { get; set; }

    }
}
