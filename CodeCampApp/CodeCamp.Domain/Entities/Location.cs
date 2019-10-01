using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// Location: Place in the planet where the CAMP will take place
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityTown { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        // Parent releationship
        // Camp is the parent class,however aslo add the ID to make 
        // life easy when fecthing the parent from the child class
        // This is important in MVC
        public Camp Camp { get; set; }
        public int CampId { get; set; }
    }
}
