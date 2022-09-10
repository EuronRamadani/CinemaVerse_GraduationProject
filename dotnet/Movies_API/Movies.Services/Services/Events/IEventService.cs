using Movies.Services.Models.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Events
{
    public interface IEventService
    {
        Task<IList<EventListModel>> GetAllAsync();
        Task<EventModel> GetAsync(int EventId);
        Task<EventModel> Create(int cinemaId, EventCreateModel eventCreateModel);
        Task<EventModel> Update(int EventId, EventCreateModel EventModel);
        Task<EventModel> Delete(int EventId);
    }
}
