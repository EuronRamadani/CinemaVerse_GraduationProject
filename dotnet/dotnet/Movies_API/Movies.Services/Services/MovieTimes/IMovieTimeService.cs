using Movies.Services.Models.Common;
using Movies.Services.Models.MovieTimes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.MovieTimes
{
    public interface IMovieTimeService
    {
        Task<IList<MovieTimeModel>> GetAllAsync(int cinemaId, int movieId);
        Task<MovieTimeModel> GetAsync(int cinemaId, int movieTimeId);
        Task<MovieTimeModel> Create(int cinemaId, int movieId, MovieTimeCreateModel movieCreateModel);
        Task<MovieTimeModel> Delete(int cinemaId, int movieTimeId);
    }
}
