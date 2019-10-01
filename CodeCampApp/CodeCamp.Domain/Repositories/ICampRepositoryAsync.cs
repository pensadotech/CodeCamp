using CodeCamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCamp.Domain.Repositories
{
    public interface ICampRepositoryAsync
    {
        // General - no need to be async
        T Add<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;

        // Commiting changes
        Task<bool> SaveChangesAsync();


        // Camp 
        Task<IEnumerable<Camp>> GetAllCampsAsync(bool includeTalks = false);
        Task<Camp> GetCampByIdAsync(string moniker, bool includeTalks = false);
        Task<IEnumerable<Camp>> GetCampsByEventDateAsync(DateTime dateTime, bool includeTalks = false);


        // Location
        Task<IEnumerable<Location>> GetAllLocationsAsync(bool includeCamps = false);
        Task<Camp> GetLocationByNameAsync(string venueName, bool includeCamps = false);
        Task<IEnumerable<Location>> GetAllLocationsByEventDateAsync(DateTime dateTime, bool includeCamps = false);


        // Talk
        Task<IEnumerable<Talk>> GetTalksInCampAsync(string campId, bool includeSpeakers = false);
        Task<Talk> GetTalkByIdCampIdAsync(string campId, int talkId, bool includeSpeakers = false);


        // Speaker
        Task<IEnumerable<Speaker>> GetAllSpeakersAsync();
        Task<Speaker> GetSpeakerAsync(int speakerId);
        Task<IEnumerable<Speaker>> GetSpeakersInCampAsync(string campId);
    }
}
