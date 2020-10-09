using CodeCamp.Domain.Entities;
using CodeCamp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CodeCamp.Data.DummyData;
using System.IO;

namespace CodeCamp.Data.Repositories
{
    /// <summary>
    /// InMemoryCampRepository - This repository is used initialy to focus teh development attention
    /// at teh web pages. The repository will load dummy data that will help working the layouts 
    /// for the frontend. Once this is completed, this will remain unused, and teh developer must 
    /// siwtch to a repostory that uses a database or the real data source.
    /// </summary>
    public class InMemoryCampRepository : ICampRepository
    {
        // Private members ...............................
        private List<Camp> _camps;  // List of camps, including Locations, Talks, and Speakers
        private List<Location> _locations;
        private List<Talk> _talks;
        private List<Speaker> _speakers;

        // Constructors ..................................
        public InMemoryCampRepository()
        {
            // Initialize camps with dummy data
            _camps = CampDummyData.GenerateDummyData();
            _locations = CampDummyData.GetLcations();
            _talks = CampDummyData.GetTalks();
            _speakers = CampDummyData.GetSpeakers();
        }

        // Methods ........................................       
        public T Add<T>(T entity) where T : class
        {   
            if (entity == null)
            {
                throw new ArgumentNullException("unable to add, entity is null");
            }

            // Obtain the type of incoming element
            Type objType = entity.GetType();
            
            // Add to proper list
            if (objType == typeof(Camp))
            {
                // Cast to Object and then to entity
                Camp newCamp = (Camp)(object)entity;

                // Add to the proper list
                _camps.Add(newCamp);

                // Determine the Max ID, add one, and assign it
                newCamp.Id = _camps.Max(camp => camp.Id) + 1;
            }
            else if (objType == typeof(Location))
            {
                // Cast to Object and then to entity
                Location newLoc = (Location)(object)entity;

                // Add to the proper list
                _locations.Add(newLoc);

                // Determine the Max ID, add one, and assign it
                newLoc.Id = _locations.Max(loc => loc.Id) + 1;
            }
            else if (objType == typeof(Talk))
            {
                // Cast to Object and then to entity
                Talk newTalk = (Talk)(object)entity;

                // Add to the proper list
                _talks.Add(newTalk);

                // Determine the Max ID, add one, and assign it
                newTalk.Id = _talks.Max(talk => talk.Id) + 1;
            }
            else if (objType == typeof(Speaker))
            {
                // Cast to Object and then to entity
                Speaker newSpeaker = (Speaker)(object)entity;

                // Add to the proper list
                _speakers.Add(newSpeaker);

                // Determine the Max ID, add one, and assign it
                newSpeaker.Id = _speakers.Max(speaker => speaker.Id) + 1;
            }

            return entity;
        }

        public T Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("unable to delete, entity is null");
            }
            
            // Obtain the type of incoming element
            Type objType = entity.GetType();

            // Delete from proper list
            if (objType == typeof(Camp))
            {
                // Cast to Object and then to entity
                Camp camp = (Camp)(object)entity;

                // Find elemen in the list
                var campToDele = GetCampById(camp.Id);

                // If element is detected, delete it from the list
                if(campToDele != null)
                {
                    _camps.Remove(campToDele);
                }
            }
            else if (objType == typeof(Location))
            {
                // Cast to Object and then to entity
                Location loc = (Location)(object)entity;

                // Find elemen in the list
                var locToDel = GetLocationById(loc.Id);

                // If element is detected, delete it from the list
                if (locToDel != null)
                {
                    _locations.Remove(locToDel);
                }
            }
            else if (objType == typeof(Talk))
            {
                // Cast to Object and then to entity
                Talk talk = (Talk)(object)entity;

                // Find elemen in the list
                var talkToDel = GetTalkById(talk.Id);

                // If element is detected, delete it from the list
                if (talkToDel != null)
                {
                    _talks.Remove(talkToDel);
                }
            }
            else if (objType == typeof(Speaker))
            {
                // Cast to Object and then to entity
                Speaker spker = (Speaker)(object)entity;

                // Find elemen in the list
                var spkerToDel = GetSpeakerById(spker.Id);

                // If element is detected, delete it from the list
                if(spkerToDel != null)
                {
                    _speakers.Remove(spkerToDel);
                }
            }

            return entity;
        }

        // Update
        public T Update<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("unable to Update, entity is null");
            }

            // Obtain the type of incoming element
            Type objType = entity.GetType();

            // Delete from proper list
            if (objType == typeof(Camp))
            {
                // Cast to Object and then to entity
                Camp camp = (Camp)(object)entity;

                // TODO: complete camp update
               
            }
            else if (objType == typeof(Location))
            {
                // Cast to Object and then to entity
                Location loc = (Location)(object)entity;

                // Find elemen in the list
                var locToUpd = GetLocationById(loc.Id);

                // If element is detected, update it 
                if (locToUpd != null)
                {
                    locToUpd.VenueName = loc.VenueName;
                    locToUpd.Address1 = loc.Address1;
                    locToUpd.Address2 = loc.Address2;
                    locToUpd.CityTown = loc.CityTown;
                    locToUpd.StateProvince = loc.StateProvince;
                    locToUpd.PostalCode = loc.PostalCode;
                    locToUpd.Country = loc.Country;
                    // Image
                    locToUpd.ProfileImageFilename = loc.ProfileImageFilename;
                    locToUpd.ProfileImageData = loc.ProfileImageData;
                }
            }
            else if (objType == typeof(Talk))
            {
                // Cast to Object and then to entity
                Talk talk = (Talk)(object)entity;

                // TODO: complete talk update
            }
            else if (objType == typeof(Speaker))
            {
                // Cast to Object and then to entity
                Speaker spker = (Speaker)(object)entity;

                // Find elemen in the list
                var spkerToUpd = GetSpeakerById(spker.Id);

                if(spkerToUpd != null)
                {
                    // If element is detected, update it 
                    spkerToUpd.FirstName = spker.FirstName;
                    spkerToUpd.LastName = spker.LastName;
                    spkerToUpd.MiddleName = spker.MiddleName;
                    spkerToUpd.Description = spker.Description;
                    spkerToUpd.Topics = spker.Topics;
                    spkerToUpd.CityTown = spker.CityTown;
                    spkerToUpd.StateProvince = spker.StateProvince;
                    spkerToUpd.Company = spker.Company;
                    spkerToUpd.CompanyUrl = spker.CompanyUrl;
                    spkerToUpd.BlogUrl = spker.BlogUrl;
                    spkerToUpd.Twitter = spker.Twitter;
                    spkerToUpd.GitHub = spker.GitHub;
                    // Image
                    spkerToUpd.ProfileImageFilename = spker.ProfileImageFilename;
                    spkerToUpd.ProfileImageData = spker.ProfileImageData;
                }

            }

            return entity;
        }

        // Commit 
        public int CommitChanges()
        {
            return 0;
        }

        // Camps
        public IEnumerable<Camp> GetAllCamps(string name = "")
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Camp> camps = from camp in _camps
                                      where string.IsNullOrEmpty(name) || camp.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)
                                      orderby camp.Name
                                      select camp;
            return camps;
        }

        public Camp GetCampById(int campId)
        {
            // Find first record that matches the incoming parameter
            Camp camp = _camps.SingleOrDefault(camp => camp.Id == campId);

            return camp;
        }

        public Camp GetCampById(string campMoniker)
        {
            // Find first record that matches the incoming parameter
            Camp camp = _camps.SingleOrDefault(camp => camp.CampCode == campMoniker);

            return camp;
        }

        public IEnumerable<Camp> GetAllCampsByEventDate(DateTime campStartDate)
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Camp> camps = from camp in _camps
                                      where camp.StartDate >= campStartDate
                                      orderby camp.Name
                                      select camp;
            return camps;
        }

        // Locations
        public IEnumerable<Location> GetAllLocations(string venueName = "")
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Location> locations = from loc in _locations
                                              where string.IsNullOrEmpty(venueName) || loc.VenueName.StartsWith(venueName, StringComparison.CurrentCultureIgnoreCase)
                                              orderby loc.VenueName
                                              select loc;
            return locations;
        }

        public Location GetLocationById(int locationId)
        {
            // Find first record that matches the incoming parameter
            Location location = _locations.FirstOrDefault(loc => loc.Id == locationId);

            return location;
        }

        // Talks
        public IEnumerable<Talk> GetAllTalks(string title = "")
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Talk> talks = from talk in _talks
                                      where string.IsNullOrEmpty(title) || talk.Title.StartsWith(title, StringComparison.CurrentCultureIgnoreCase)
                                      orderby talk.Title
                                      select talk;
            return talks;
        }

        public Talk GetTalkById(int talkId)
        {
            // Find first record that matches the incoming parameter
            Talk talk = _talks.FirstOrDefault(talk => talk.Id == talkId);

            return talk;
        }

        // Speakers
        public IEnumerable<Speaker> GetAllSpeakers(string lastName = "")
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Speaker> speakers = from speaker in _speakers
                                            where string.IsNullOrEmpty(lastName) || speaker.LastName.StartsWith(lastName, StringComparison.InvariantCultureIgnoreCase)
                                            orderby speaker.LastName
                                            select speaker;
            return speakers;
        }

        public Speaker GetSpeakerById(int speakerId)
        {
            // Find first record that matches the incoming parameter
            Speaker speaker = _speakers.FirstOrDefault(speaker => speaker.Id == speakerId);

            return speaker;
        }

        public int GetCount()
        {
            return _speakers.Count;
        }

    }
}
