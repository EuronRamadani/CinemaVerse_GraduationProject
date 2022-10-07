using Movies.Services.Models.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Events
{
    public interface IEventService
    {
        Task<IList<EventListModel>> GetAllAsync(int cinemaId);
        Task<EventModel> GetAsync(int cinemaId, int eventId);
        Task<EventModel> Create(int cinemaId, EventCreateModel eventCreateModel);
        Task<EventModel> Update(int eventId, int cinemaId, EventCreateModel EventModel);
        Task<EventModel> Delete(int cinemaId, int eventId);
    }
}
