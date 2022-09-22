using Movies.Services.Models.Rows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Rows
{
    public interface IRowService
    {
        Task<IList<RowModel>> GetAllAsync(int cinemaId, int hallId);
        Task<RowModel> GetAsync(int cinemaId, int hallId, int rowId);
        Task<RowModel> Create(int cinemaId, int hallId, RowCreateModel rowCreateModel);
        Task<RowModel> Update(int cinemaId, int hallId, int rowId, RowCreateModel rowModel);
        Task<RowModel> Delete(int cinemaId, int hallId, int rowId);
    }
}
