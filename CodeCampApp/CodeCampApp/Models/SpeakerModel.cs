using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

        [StringLength(100)]
        public string Topics { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(100)]
        public string CompanyUrl { get; set; }

        [StringLength(100)]
        public string BlogUrl { get; set; }

        [StringLength(100)]
        public string Twitter { get; set; }

        [StringLength(100)]
        public string GitHub { get; set; }

        [Required, StringLength(150)]
        public string CityTown { get; set; }

        [Required, StringLength(50)]
        public string StateProvince { get; set; }

        public string ProfileImage { get; set; }
    }
}
