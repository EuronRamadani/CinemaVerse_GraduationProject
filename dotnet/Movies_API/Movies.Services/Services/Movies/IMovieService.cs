using Movies.Core.Domain;
using Movies.Services.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Movies
{
    public interface IMovieService
    {
        Task<IList<MovieListModel>> GetAllAsync();
        Task<MovieModel> GetAsync(int movieId);
        Task<MovieModel> Create(MovieCreateModel movieCreateModel);
        Task<MovieModel> Update(int movieId, MovieCreateModel movieModel);
        Task<MovieModel> Delete(int movieId);
    }
}
