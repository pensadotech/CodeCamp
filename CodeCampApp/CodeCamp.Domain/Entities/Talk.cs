using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCamp.Domain.Entities
{
    /// <summary>
    /// File: Talk
    /// Purpose : Talk entity that represent a learning session in a coding camp.
    /// </summary>
    public class Talk
    {
        public int Id { get; set; }

        // TalkCode is a moniker is an alterntive ID to be used from website, instead of using the ID
        public string TalkCode { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Track { get; set; }
        public int Level { get; set; }
        public string Room { get; set; }

        public string WeekDays { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public string StartTime { get; set; }
        public int LengthInWeeks { get; set; } = 1;

        // Image profile
        public string ProfileImageFilename { get; set; }
        public byte[] ProfileImageData { get; set; }

        // Parent releationship
        // Camp is the parent class,however aslo add the ID to make 
        // life easy when fecthing the parent from the child class
        // This is important in MVC
        public Camp Camp { get; set; }
        public int CampId { get; set; }

        // Chidlren relationship (one-to-one) 
        public Speaker Speaker { get; set; }
    }
}
