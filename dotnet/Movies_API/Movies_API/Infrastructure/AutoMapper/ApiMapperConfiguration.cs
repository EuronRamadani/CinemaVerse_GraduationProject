using AutoMapper;
using Movies.Core.Domain;
using Movies.Services.Models.Cinemas;
using Movies.Services.Models.Movies;

namespace Movies.Api.Infrastructure.AutoMapper
{
    public class ApiMapperConfiguration : Profile
    {
        public ApiMapperConfiguration()
        {
            CreateMovieMapper();
            CreateCinemaMapper();
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

        private void CreateCinemaMapper()
        {
            CreateMap<Cinema, CinemaModel>()
                .ForMember(x => x.Movies, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Cinema, CinemaListModel>()
                .ReverseMap();

            CreateMap<CinemaCreateModel, Cinema>()
                .ForMember(x => x.Movies, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CinemaModel, CinemaCreateModel>()
                .ReverseMap();
        }
    }
}
