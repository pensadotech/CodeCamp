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
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        Task<bool> CommitChanges();
        
        // Camp 
        Task<IEnumerable<Camp>> GetAllCamps(string name = "");
        Task<Camp> GetCampById(string moniker);
        Task<IEnumerable<Camp>> GetAllCampsByEventDate(DateTime dateTime);

        // Location
        Task<IEnumerable<Location>> GetAllLocations(string venueName = "");
        Task<Location> GetLocationById(int locationId);

        // Talk
        Task<IEnumerable<Talk>> GetAllTalks(string title = "");
        Task<Talk> GetTalkById(int talkId);

        // Speaker
        Task<IEnumerable<Speaker>> GetAllSpeakers(string speakerLastName = "");
        Task<Speaker> GetSpeakerById(int speakerId);
        
    }
}
