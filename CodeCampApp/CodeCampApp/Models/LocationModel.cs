using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCampApp.Models
{
    public class LocationModel
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string VenueName { get; set; }

        [Required, StringLength(150)]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        
        public string Address3 { get; set; }

        [Required, StringLength(150)]
        public string CityTown { get; set; }

        [Required, StringLength(150)]
        public string StateProvince { get; set; }

        [Required, StringLength(10)]
        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
