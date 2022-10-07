using Movies.Services.Models.Actors;
using Movies.Services.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Services.Services.Movies
{
    public interface IMovieService
    {
        Task<IList<MovieListModel>> GetAllAsync(int cinemaId);
        Task<IList<ActorListModel>> GetAllActorsAsync(int movieId);
        Task<MovieModel> GetAsync(int cinemaId, int movieId);
        Task<MovieModel> Create(int cinemaId, MovieCreateModel movieCreateModel);
        Task<MovieModel> Update(int cinemaId, int movieId, MovieCreateModel movieModel);
        Task<MovieModel> Delete(int cinemaId, int movieId);
    }
}
