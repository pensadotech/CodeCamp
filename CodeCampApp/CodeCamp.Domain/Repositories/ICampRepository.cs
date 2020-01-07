using CodeCamp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CodeCamp.Domain.Repositories
{
    /// <summary>
    /// File: ICampRepository
    /// Purpose: This repository represent Non-Async functions to handle the entities 
    ///          assoicated with the project.
    /// Notes:   This interface helps to start coding the front end before the DB access
    ///          is formalized using aync operations
    /// </summary>
    public interface ICampRepository
    {
        // General 
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        void CommitChanges();


        // Camp 
        IEnumerable<Camp> GetAllCampsByName(string name, bool includeTalks = false);
        Camp GetCampById(string moniker, bool includeTalks = false);
        IEnumerable<Camp> GetAllCampsByEventDate(DateTime dateTime, bool includeTalks = false);

        // Location
        IEnumerable<Location> GetAllLocations(bool includeCamps = false);
        Location GetLocationByName(string venueName, bool includeCamps = false);
        IEnumerable<Location> GetAllLocationsByEventDate(DateTime dateTime, bool includeCamps = false);


        // Talk
        IEnumerable<Talk> GetAllTalksInCamp(string campId, bool includeSpeakers = false);
        Talk GetTalkById(string campId, int talkId, bool includeSpeakers = false);

        // Speaker
        IEnumerable<Speaker> GetAllSpeakers();
        Speaker GetSpeakerById(int speakerId);
        IEnumerable<Speaker> GetAllSpeakersInCamp(string campId);


    }
}
