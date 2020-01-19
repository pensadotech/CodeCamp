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
        // Generic entries 
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        int CommitChanges();
        
        // Camp 
        IEnumerable<Camp> GetAllCampsByName(string name);
        Camp GetCampById(int campId);
        Camp GetCampById(string moniker);
        IEnumerable<Camp> GetAllCampsByEventDate(DateTime dateTime);

        // Location
        IEnumerable<Location> GetAllLocations(string venueName);
        Location GetLocationById(int locationId);

        // Talk
        IEnumerable<Talk> GetAllTalks(string title);
        Talk GetTalkById (int talkId);

        // Speaker
        IEnumerable<Speaker> GetAllSpeakers(string speakerLastName);
        Speaker GetSpeakerById(int speakerId);


    }
}
