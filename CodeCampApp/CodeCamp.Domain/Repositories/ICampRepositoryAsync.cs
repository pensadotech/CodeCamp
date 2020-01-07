using CodeCamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCamp.Domain.Repositories
{
    /// <summary>
    /// File: ICampRepository
    /// Purpose: This repository represent Async functions to handle the entities 
    ///          assoicated with the project.
    /// Notes:   This interface represent the formal aproach to retreive data from 
    ///          the database, accounting for delays.
    ///          Insted of obtaning objects directly, the object retrieval is inside
    ///          a Threading Task
    /// </summary>
    public interface ICampRepositoryAsync
    {
        // General - no need to be async
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        Task<bool> CommitChangesAsync();


        // Camp 
        Task<IEnumerable<Camp>> GetAllCampsByNameAsync(string name,bool includeTalks = false);
        Task<Camp> GetCampByIdAsync(string moniker, bool includeTalks = false);
        Task<IEnumerable<Camp>> GetAllCampsByEventDateAsync(DateTime dateTime, bool includeTalks = false);


        // Location
        Task<IEnumerable<Location>> GetAllLocationsAsync(bool includeCamps = false);
        Task<Location> GetLocationByNameAsync(string venueName, bool includeCamps = false);
        Task<IEnumerable<Location>> GetAllLocationsByEventDateAsync(DateTime dateTime, bool includeCamps = false);


        // Talk
        Task<IEnumerable<Talk>> GetTalksInCampAsync(string campId, bool includeSpeakers = false);
        Task<Talk> GetTalkByIdAsync(string campId, int talkId, bool includeSpeakers = false);


        // Speaker
        Task<IEnumerable<Speaker>> GetAllSpeakersAsync();
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<IEnumerable<Speaker>> GetSpeakersInCampAsync(string campId);
    }
}
