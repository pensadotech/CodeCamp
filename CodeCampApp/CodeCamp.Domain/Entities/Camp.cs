using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// Code camp : Coding camp name and charactersitics
    /// </summary>
    public class Camp
    {
        public int Id { get; set; }

        // Moniker is an alterntive ID to be used from website, instead of using teh ID
        [Required, StringLength(50)]
        public string Moniker { get; set; }

        public string Name { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        public int Length { get; set; } = 1;

        // Chidlren relationship (one-to-one) 
        public Location Location { get; set; }

        // Children relationship (one-to-many)
        public ICollection<Talk> Talks { get; set; }
    }
}
