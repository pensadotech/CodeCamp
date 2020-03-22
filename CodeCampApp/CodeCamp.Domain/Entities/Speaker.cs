using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// File: Speaker
    /// Purpose : Speaker entity that represent the person imparting a session in the code camp.
    /// </summary>
    public class Speaker
    {
        public int Id { get; set; }
      
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }

        public string Topics { get; set; }
        
        public string Company { get; set; }

        public string CompanyUrl { get; set; }

        public string BlogUrl { get; set; }
        
        public string Twitter { get; set; }
        
        public string GitHub { get; set; }

        public string CityTown { get; set; }

        public string StateProvince { get; set; }

        public string ProfileImage { get; set; }

        // Parent releationship
        // Camp is the parent class,however aslo add the ID to make 
        // life easy when fecthing the parent from the child class
        // This is important in MVC
        public Talk Talk { get; set; }
        public int TalkId { get; set; }
    }
}
