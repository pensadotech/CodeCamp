using CodeCamp.Domain.Entities;
using CodeCamp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CodeCamp.Data.DummyData;

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

                // Convert back to entity, is this needed?
                entity = (T)(object)newCamp;

            }
            else if (objType == typeof(Location))
            {
                // Cast to Object and then to entity
                Location newLoc = (Location)(object)entity;

                // Add to the proper list
                _locations.Add(newLoc);

                // Determine the Max ID, add one, and assign it
                newLoc.Id = _locations.Max(loc => loc.Id) + 1;

                // Convert back to entity, is this needed?
                entity = (T)(object)newLoc;

            }
            else if (objType == typeof(Talk))
            {
                // Cast to Object and then to entity
                Talk newTalk = (Talk)(object)entity;

                // Add to the proper list
                _talks.Add(newTalk);

                // Determine the Max ID, add one, and assign it
                newTalk.Id = _talks.Max(talk => talk.Id) + 1;

                // Convert back to entity, is this needed?
                entity = (T)(object)newTalk;
            }
            else if (objType == typeof(Speaker))
            {
                // Cast to Object and then to entity
                Speaker newSpeaker = (Speaker)(object)entity;

                // Add to the proper list
                _speakers.Add(newSpeaker);

                // Determine the Max ID, add one, and assign it
                newSpeaker.Id = _speakers.Max(speaker => speaker.Id) + 1;

                // Convert back to entity, is this needed?
                entity = (T)(object)newSpeaker;
            }

            return entity;
        }

        public T Update<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("unable to update, entity is null");
            }

            // Obtain the type of incoming element
            Type objType = entity.GetType();

            // Delete from proper list
            if (objType == typeof(Camp))
            {
                // Cast to Object and then to entity
                Camp tgtCamp = (Camp)(object)entity;

                var campToUpd = _camps.SingleOrDefault(camp => camp.Id == tgtCamp.Id);

                if (campToUpd != null)
                {
                    campToUpd.Name = tgtCamp.Name;
                    campToUpd.Moniker = tgtCamp.Moniker;
                    campToUpd.EventDate = tgtCamp.EventDate;
                    campToUpd.Location = tgtCamp.Location;
                    campToUpd.Talks = tgtCamp.Talks;
                }

                // Convert back to entity, is this needed?
                entity = (T)(object)campToUpd;

            }
            else if (objType == typeof(Location))
            {

            }
            else if (objType == typeof(Talk))
            {

            }
            else if (objType == typeof(Speaker))
            {

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
                Camp tgtCamp = (Camp)(object)entity;

                // Find elemen in teh list
                var campToDelete = _camps.SingleOrDefault(camp => camp.Id == tgtCamp.Id);

                // If element is detected, delete it from the list
                if(campToDelete != null)
                {
                    _camps.Remove(campToDelete);
                }

                entity = (T)(object)campToDelete;

            }
            else if (objType == typeof(Location))
            {
                
            }
            else if (objType == typeof(Talk))
            {
                
            }
            else if (objType == typeof(Speaker))
            {
                
            }

            return entity;
        }

        // Commit 
        public int CommitChanges()
        {
            return 0;
        }

        // Camps
        public IEnumerable<Camp> GetAllCampsByName(string name)
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
            Camp camp = _camps.SingleOrDefault(camp => camp.Moniker == campMoniker);

            return camp;
        }

        public IEnumerable<Camp> GetAllCampsByEventDate(DateTime campEventDate)
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Camp> camps = from camp in _camps
                                      where camp.EventDate >= campEventDate
                                      orderby camp.Name
                                      select camp;
            return camps;
        }

        // Locations
        public IEnumerable<Location> GetAllLocations(string venueName)
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
        IEnumerable<Talk> GetAllTalks(string talkTitle)
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Talk> talks = from talk in _talks
                                      where string.IsNullOrEmpty(talkTitle) || talk.Title.StartsWith(talkTitle, StringComparison.CurrentCultureIgnoreCase)
                                      orderby talk.Title
                                      select talk;
            return talks;
        }

        Talk GetTalkById(int talkId)
        {
            // Find first record that matches the incoming parameter
            Talk talk = _talks.FirstOrDefault(talk => talk.Id == talkId);

            return talk;
        }

        // Speakers
        public IEnumerable<Speaker> GetAllSpeakers(string speakerLastName)
        {
            // Get all records from the list, but filter if incoming parameters has a value
            IEnumerable<Speaker> speakers = from speaker in _speakers
                                            where string.IsNullOrEmpty(speakerLastName) || speaker.LastName.StartsWith(speakerLastName, StringComparison.InvariantCultureIgnoreCase)
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

        IEnumerable<Talk> ICampRepository.GetAllTalks(string title)
        {
            throw new NotImplementedException();
        }

        Talk ICampRepository.GetTalkById(int talkId)
        {
            throw new NotImplementedException();
        }
    }
}
