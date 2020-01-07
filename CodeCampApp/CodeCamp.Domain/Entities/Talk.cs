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
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }

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
