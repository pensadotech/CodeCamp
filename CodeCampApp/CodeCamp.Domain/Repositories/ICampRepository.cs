using CodeCamp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CodeCamp.Domain.Repositories
{
    public interface ICampRepository
    {

        // General 
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        void SaveChanges();


        // Camp 
        IEnumerable<Camp> GetAllCamps(bool includeTalks = false);
        Camp GetCampById(string moniker, bool includeTalks = false);
        IEnumerable<Camp> GetCampsByEventDate(DateTime dateTime, bool includeTalks = false);

        // Location
        IEnumerable<Location> GetAllLocations(bool includeCamps = false);
        Camp GetLocationByName(string venueName, bool includeCamps = false);
        IEnumerable<Location> GetAllLocationsByEventDate(DateTime dateTime, bool includeCamps = false);


        // Talk
        IEnumerable<Talk> GetTalksInCamp(string campId, bool includeSpeakers = false);
        Talk GetTalkByIdCampId(string campId, int talkId, bool includeSpeakers = false);

        // Speaker
        IEnumerable<Speaker> GetAllSpeakers();
        Speaker GetSpeaker(int speakerId);
        IEnumerable<Speaker> GetSpeakersInCamp(string campId);


    }
}
