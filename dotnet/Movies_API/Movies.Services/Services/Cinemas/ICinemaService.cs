using Movies.Services.Models.Cinemas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Cinemas
{
    public interface ICinemaService
    {
        Task<IList<CinemaListModel>> GetAllAsync();
        Task<CinemaModel> GetAsync(int cinemaId);
        Task<CinemaModel> Create(CinemaCreateModel movieCreateModel);
        Task<CinemaModel> Update(int cinemaId, CinemaCreateModel cinemaModel);
        Task<CinemaModel> Delete(int cinemaId);
    }
}
