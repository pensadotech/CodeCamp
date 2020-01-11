using CodeCamp.Domain.Entities;
using CodeCamp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CodeCamp.Data.DummyData;

namespace CodeCamp.Data.Repositories
{
    public class InMemoryCampRepository : ICampRepository
    {
        // Private members ...............................
        private IEnumerable<Camp> _camps;

        // Constructors ..................................
        public InMemoryCampRepository()
        {
            // Initialize camps with dummy data
            _camps = CampDummyData.GenerateDummyData();                 
        }
        
        // Methods ........................................       
        public T Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public T Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        // Commit 
        public void CommitChanges()
        {
            throw new NotImplementedException();
        }

        // Camps
        public IEnumerable<Camp> GetAllCampsByName(string name, bool includeTalks= true)
        {  
            // Collect all camps from local list
            IEnumerable<Camp> camps = from c in _camps
                                      where string.IsNullOrEmpty(name) || c.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)
                                      orderby c.Name
                                      select c;

            // If request does not want talk in the camp, remove them
            if (!includeTalks)
            {
                foreach (var camp in camps)
                {
                    camp.Talks = null;
                }
            }

            return camps;
        }

        public Camp GetCampById(string moniker, bool includeTalks = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Camp> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false)
        {
            throw new NotImplementedException();
        }

        // Locations
        public IEnumerable<Location> GetAllLocations(bool includeCamps = false)
        {
            throw new NotImplementedException();
        }

        public Location GetLocationByName(string venueName, bool includeCamps = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAllLocationsByEventDate(DateTime dateTime, bool includeCamps = false)
        {
            throw new NotImplementedException();
        }

        // Talks
        public IEnumerable<Talk> GetAllTalksInCamp(string campId, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        public Talk GetTalkById(string campId, int talkId, bool includeSpeakers = false)
        {
            throw new NotImplementedException();
        }

        // Speakers
        public IEnumerable<Speaker> GetAllSpeakers()
        {
            throw new NotImplementedException();
        }

        public Speaker GetSpeakerById(int speakerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Speaker> GetAllSpeakersInCamp(string campId)
        {
            throw new NotImplementedException();
        }
    }
}
