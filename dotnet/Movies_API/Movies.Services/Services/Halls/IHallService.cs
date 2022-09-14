using Movies.Services.Models.Halls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Halls
{
    public interface IHallService
    {
        Task<IList<HallListModel>> GetAllAsync(int cinemaId);
        Task<HallModel> GetAsync(int cinemaId , int hallId);
        Task<HallModel> Create(int cinemaId, HallCreateModel hallCreateModel);
        Task<HallModel> Update(int cinemaId, int hallId, HallCreateModel hallModel);
        Task<HallModel> Delete(int cinemaId, int hallId);
    }
}
