using AutoMapper;
using Movies.Core.Domain;
using Movies.Services.Models.Movies;

namespace Movies.Api.Infrastructure.AutoMapper
{
    public class ApiMapperConfiguration : Profile
    {
        public ApiMapperConfiguration()
        {
            CreateMovieMapper();
        }

        private void CreateMovieMapper()
        {
            CreateMap<Movie, MovieModel>()
                .ReverseMap();

            CreateMap<Movie, MovieListModel>()
                .ReverseMap();

            CreateMap<MovieCreateModel, Movie>()
                .ReverseMap();            
            
            CreateMap<MovieModel, MovieCreateModel>()
                .ReverseMap();

        }
    }
}
