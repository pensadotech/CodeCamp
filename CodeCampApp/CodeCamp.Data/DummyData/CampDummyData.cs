using System;
using System.Collections.Generic;
using System.Text;
using CodeCamp.Domain.Entities;

namespace CodeCamp.Data.DummyData
{
    public static class CampDummyData
    {
        public static List<Camp> GenerateDummyData()
        {
            // List of camps
            var camps = GetCamps();
            // List of locations
            var locations = GetLcations();
            // Speakers
            var speakers = GetSpeakers();
            // Talks
            var talks = GetTalks();

            // Associate elements for the camps

            // Camp #1 
            // The camp #1 has one location -> Location #1
            camps[0].Location = locations[0];
            // Set camp #1 in Location #1 also
            locations[0].Camp = camps[0];
            locations[0].CampId = camps[0].Id;

            // Talk #1 has one Speakers -> speaker #1
            talks[0].Speaker = speakers[0];
            // Set Talk #1 in Speaker #1 also
            speakers[0].Talk = talks[0];
            speakers[0].TalkId = talks[0].Id;

            // Talk #2 has one speaker -> Speaker #2
            talks[1].Speaker = speakers[1];
            // Set Talk #2 in Speaker #2 also
            speakers[1].Talk = talks[1];
            speakers[1].TalkId = talks[1].Id;

            // Camp #1 has Talks (Talk #1 and Talk #2)
            camps[0].Talks = new List<Talk>()
            {
                talks[0], talks[1]
            };

            // Set camp #1 in Talks #1 and #2 also
            talks[0].Camp = camps[0];
            talks[0].CampId = camps[0].Id;
            talks[1].Camp = camps[0];
            talks[1].CampId = camps[0].Id;


            // Camp #2
            // The camp #2 has one location -> Location #2
            camps[1].Location = locations[1];
            // Set camp #1 in Location #1 also
            locations[1].Camp = camps[1];
            locations[1].CampId = camps[1].Id;

            // Talk #3 has one Speakers -> speaker #3
            talks[2].Speaker = speakers[2];
            // Set Talk #1 in Speaker #1 also
            speakers[2].Talk = talks[2];
            speakers[2].TalkId = talks[2].Id;

            // Talk #4 has one speaker -> Speaker #4
            talks[3].Speaker = speakers[3];
            // Set Talk #4 in Speaker #4 also
            speakers[3].Talk = talks[3];
            speakers[3].TalkId = talks[3].Id;

            // Camp #3 has Talks (Talk #3 and Talk #4)
            camps[1].Talks = new List<Talk>()
            {
                talks[2], talks[3]
            };

            // Set camp #2 in Talks #3 and #4 also
            talks[2].Camp = camps[1];
            talks[2].CampId = camps[1].Id;
            talks[3].Camp = camps[1];
            talks[3].CampId = camps[1].Id;
            

            return camps;
        }

        // Camps
        public static List<Camp> GetCamps()
        {
            var camps = new List<Camp>();

            // Camp #1
            var camp1 = new Camp()
            {
                Id = 1,
                Moniker = "ATL2019",
                Name = "Xtreme Code Camp",
                EventDate = new DateTime(2019, 11, 15),
                Length = 12
            };

            // Camp #2
            var camp2 = new Camp()
            {
                Id = 1,
                Moniker = "ATL2020",
                Name = "UCI Code Camp",
                EventDate = new DateTime(2020, 1, 20),
                Length = 12
            };

            // Add camps tp the list
            camps.Add(camp1);
            camps.Add(camp2);

            return camps;
        }

        // Locations
        public static List<Location> GetLcations()
        {
            var Locations = new List<Location>();

            // Location #1 
            var location1 = new Location()
            {
                Id = 1,
                VenueName = "Convention Center",
                Address1 = "123 Main Street",
                Address2 = "",
                Address3 = "",
                CityTown = "Irvine",
                StateProvince = "CA",
                PostalCode = "12345",
                Country = "USA"
            };

            // Location #2
            var location2 = new Location()
            {
                Id = 2,
                VenueName = "UCI extension",
                Address1 = "345 Protola Street",
                Address2 = "",
                Address3 = "",
                CityTown = "Tustin",
                StateProvince = "CA",
                PostalCode = "54321",
                Country = "USA"
            };

            // Add locaion to List
            Locations.Add(location1);
            Locations.Add(location2);

            return Locations;
        }

        // Talks
        public static List<Talk> GetTalks()
        {
            var talks = new List<Talk>();

            // Talk #1
            var talk1 = new Talk()
            {
                Id = 1,
                Title = "Entity Framework From Scratch",
                Abstract = "Entity Framework from scratch in an hour. Probably cover it all.",
                Level = 100
            };

            // Talk #2
            var talk2 = new Talk()
            {
                Id = 2,
                Title = "API From Scratch",
                Abstract = "API advance topic, all you need to know. ",
                Level = 200
            };

            // Talk #3
            var talk3 = new Talk()
            {
                Id = 3,
                Title = "Entity Framework From Scratch",
                Abstract = "Entity Framework from scratch in an hour. Probably cover it all.",
                Level = 100
            };

            // Talk #4
            var talk4 = new Talk()
            {
                Id = 4,
                Title = "API From Scratch",
                Abstract = "API advance topic, all you need to know. ",
                Level = 200
            };

            // Add talks to list 
            talks.Add(talk1);
            talks.Add(talk2);
            talks.Add(talk3);
            talks.Add(talk4);

            return talks;
        }

        // Speakers
        public static List<Speaker> GetSpeakers()
        {
            var speakers = new List<Speaker>();

            // Sepeaker #1
            var speaker1 = new Speaker()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                MiddleName = "R",
                BlogUrl = "http://JohnDoeBlog.com",
                Company = "Wilder Minds LLC",
                CompanyUrl = "http://JohnDoe.com",
                GitHub = "http://JohnDoe.github.io",
                Twitter = "@johnDoe"
            };

            // Sepeaker #2
            var speaker2 = new Speaker()
            {
                Id = 2,
                FirstName = "Susan",
                LastName = "Flowers",
                MiddleName = "V",
                BlogUrl = "http://SusanFlowersBlog.com",
                Company = "Susan LLC",
                CompanyUrl = "http://SusanFlowers.com",
                GitHub = "http://susan.github.io",
                Twitter = "@SusanFlowers"
            };

            // Sepeaker #3
            var speaker3 = new Speaker()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Freak",
                MiddleName = "S",
                BlogUrl = "http://JohnFreakBlog.com",
                Company = "Wilder Minds LLC",
                CompanyUrl = "http://JohFreak.com",
                GitHub = "http://JohnFreak.github.io",
                Twitter = "@JohnFreak"
            };

            // Sepeaker #4
            var speaker4 = new Speaker()
            {
                Id = 2,
                FirstName = "Rosie",
                LastName = "Lavander",
                MiddleName = "",
                BlogUrl = "http://RosieLavanderBlog.com",
                Company = "Susan LLC",
                CompanyUrl = "http://RosieLavander.com",
                GitHub = "http://RosieLavander.github.io",
                Twitter = "@RosieLavander"
            };


            // Add speakers to the list 
            speakers.Add(speaker1);
            speakers.Add(speaker2);
            speakers.Add(speaker3);
            speakers.Add(speaker4);

            return speakers;
        }



    }
}
