using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// File: Camp
    /// Purpose : Camp entity that represent a coding workshop.
    /// </summary>
    public class Camp
    {
        public int Id { get; set; }

        // CampCode is a moniker is an alterntive ID to be used from website, instead of using the ID
        public string CampCode { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public int LengthInWeeks { get; set; } = 1;

        // Chidlren relationship (one-to-one) 
        public Location Location { get; set; }

        // Children relationship (one-to-many)
        public ICollection<Talk> Talks { get; set; }
    }
}
