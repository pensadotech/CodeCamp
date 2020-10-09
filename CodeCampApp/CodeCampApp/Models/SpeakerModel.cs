using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeCampApp.Models
{
    public class SpeakerModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Topics { get; set; }
        [Required, StringLength(150)]
        public string CityTown { get; set; }

        [Required, StringLength(20)]
        public string StateProvince { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(200)]
        public string CompanyUrl { get; set; }

        [StringLength(200)]
        public string BlogUrl { get; set; }

        [StringLength(200)]
        public string Twitter { get; set; }

        [StringLength(200)]
        public string GitHub { get; set; }

      
        // Image profile
        public string ProfileImageFilename { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImageFormFile { get; set; }
    }
}
