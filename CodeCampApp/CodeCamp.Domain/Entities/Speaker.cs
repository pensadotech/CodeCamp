using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// Speaker represent the person imparting a Talk
    /// </summary>
    public class Speaker
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }
        
        [Required, StringLength(50)]
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        public string CompanyUrl { get; set; }

        public string BlogUrl { get; set; }
        
        public string Twitter { get; set; }
        
        public string GitHub { get; set; }

        // Parent releationship
        // Camp is the parent class,however aslo add the ID to make 
        // life easy when fecthing the parent from the child class
        // This is important in MVC
        public Talk Talk { get; set; }
        public int TalkID { get; set; }
    }
}
