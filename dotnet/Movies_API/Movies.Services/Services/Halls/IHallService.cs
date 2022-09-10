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
        Task<IList<HallListModel>> GetAllAsync();
        Task<HallModel> GetAsync(int hallId);
        Task<HallModel> Create(HallCreateModel hallCreateModel);
        Task<HallModel> Update(int hallId, HallCreateModel hallModel);
        Task<HallModel> Delete(int hallId);
    }
}
