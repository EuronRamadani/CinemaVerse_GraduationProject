using Movies.Services.Models.Actors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Actors
{
    public interface IActorService
    {
        Task<IList<ActorListModel>> GetAllAsync();
        Task<ActorModel> GetAsync(int actorId);
        Task<ActorModel> Create(ActorCreateModel actorCreateModel);
        Task<ActorModel> Update(int actorId, ActorCreateModel actorModel);
        Task<ActorModel> Delete(int actorId);
    }
}
