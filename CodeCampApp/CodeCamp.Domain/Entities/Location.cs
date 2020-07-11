using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// File: Location
    /// Purpose : Location entity that represent the place where a coding camp will occur.
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityTown { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ContactPhone { get; set; }
        public string ProfileImageFilename { get; set; }
        public byte[] ProfileImageData { get; set; }
       
        // Parent releationship
        // Camp is the parent class,however aslo add the ID to make 
        // life easy when fecthing the parent from the child class
        // This is important in MVC
        public Camp Camp { get; set; }
        public int CampId { get; set; }
    }
}
