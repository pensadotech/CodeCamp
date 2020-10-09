using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeCampApp.Models
{
    public class TalkModel
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        // TalkCode is a moniker is an alterntive ID to be used from website, instead of using the ID
        public string TalkCode { get; set; }
        [Required,StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(80)]
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

        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImageFormFile { get; set; }

        // Speaker details ............................................
        public int SpeakerId { get; set; }
        public string SpeakerFirstName {get; set;}
        public string SpeakerMiddleName { get; set; }

        public string SpeakerLastName { get; set; }

        public string SpeakerCityTown { get; set; }

        public string SpeakerStateProvince { get; set; }

        public string SpeakerCompany { get; set; }
        public string SpeakerCompanyUrl { get; set; }
        public string SpeakerBlogUrl { get; set; }
        public string SpeakerTwitter { get; set; }
        public string SpeakerGitHub { get; set; }

        // Image profile
        public string SpeakerProfileImageFilename { get; set; }

    }
}
