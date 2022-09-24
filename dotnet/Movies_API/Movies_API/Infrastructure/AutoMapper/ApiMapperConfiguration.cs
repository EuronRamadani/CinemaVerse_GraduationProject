using AutoMapper;
using Movies.Core.Domain;
using Movies.Services.Models.Actors;
using Movies.Services.Models.Cinemas;
using Movies.Services.Models.Halls;
using Movies.Services.Models.Events;
using Movies.Services.Models.Movies;
using Movies.Services.Models.Photos;
using Movies.Services.Models.Reviews;
using Movies.Services.Models.MovieTimes;
using Movies.Services.Models.Rows;
using Movies.Services.Models.Seats;

namespace Movies.Api.Infrastructure.AutoMapper
{
    public class ApiMapperConfiguration : Profile
    {
        public ApiMapperConfiguration()
        {
            CreateMovieMapper();
            CreateCinemaMapper();
            CreatePhotoMapper();
            CreateHallMapper();
            CreateActorMapper();
            CreateEventMapper();
            CreateReviewMapper();
            CreateMovieTimeMapper();
            CreateRowMapper();
            CreateSeatMapper();
        }

        private void CreateActorMapper()
        {
            CreateMap<Actor, ActorModel>()
               .ReverseMap();

            CreateMap<Actor, ActorListModel>()
                .ReverseMap();

            CreateMap<ActorCreateModel, Actor>()
                .ReverseMap();

            CreateMap<ActorModel, ActorCreateModel>()
                .ReverseMap();
        }

        private void CreateReviewMapper()
        {
            CreateMap<MovieReview, MovieReviewModel>()
               .ReverseMap();

            CreateMap<MovieReview, MovieReviewListModel>()
                .ReverseMap();

            CreateMap<MovieReviewCreateModel, MovieReview>()
                .ReverseMap();

            CreateMap<MovieReviewModel, MovieReviewCreateModel>()
                .ReverseMap();
        }

        private void CreateHallMapper()
        {
            CreateMap<Hall, HallModel>()
                .ReverseMap();

            CreateMap<Hall, HallListModel>()
                .ReverseMap();

            CreateMap<HallCreateModel, Hall>()
                .ReverseMap();

            CreateMap<HallModel, HallCreateModel>()
                .ReverseMap();
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
                .ReverseMap();

            CreateMap<Cinema, CinemaListModel>()
                .ReverseMap();

            CreateMap<CinemaCreateModel, Cinema>()
                .ReverseMap();

            CreateMap<CinemaModel, CinemaCreateModel>()
                .ReverseMap();
        }

        private void CreateEventMapper()
        {
            CreateMap<Event, EventModel>()
                .ReverseMap();

            CreateMap<Event, EventListModel>()
                .ReverseMap();

            CreateMap<EventCreateModel, Event>()
                .ReverseMap();

            CreateMap<EventModel, EventCreateModel>()
                .ReverseMap();
        }

        private void CreateMovieTimeMapper()
        {
            CreateMap<MovieTime, MovieTimeModel>()
                .ReverseMap();

            CreateMap<MovieTimeCreateModel, MovieTime>()
                .ReverseMap();

            CreateMap<MovieTimeModel, MovieTimeCreateModel>()
                .ReverseMap();
        }       
        
        private void CreateRowMapper()
        {
            CreateMap<Row, RowModel>()
                .ReverseMap();

            CreateMap<RowCreateModel, Row>()
                .ReverseMap();

            CreateMap<RowModel, RowCreateModel>()
                .ReverseMap();
        }
        
        private void CreateSeatMapper()
        {
            CreateMap<Seat, SeatModel>()
                .ReverseMap();

            CreateMap<SeatCreateModel, Seat>()
                .ReverseMap();

            CreateMap<SeatModel, SeatCreateModel>()
                .ReverseMap();
        }

        private void CreatePhotoMapper()
        {
            CreateMap<Photo, PhotoModel>()
                .ReverseMap();
        }
    }
}
