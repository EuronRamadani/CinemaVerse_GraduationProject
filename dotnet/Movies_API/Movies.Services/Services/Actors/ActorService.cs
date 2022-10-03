using AutoMapper;
using Microsoft.Extensions.Logging;
using Movies.Core.Domain;
using Movies.Core.Exceptions;
using Movies.Data.Interfaces;
using Movies.Services.Models.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Services.Actors
{
    public class ActorService : IActorService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Actor> _actorRepository;
        private readonly ILogger<ActorService> _logger;
        public ActorService(
           IMapper mapper,
           IRepository<Actor> actorRepository,
           ILogger<ActorService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<ActorListModel>> GetAllAsync()
        {
            try
            {
                var actors = await _actorRepository.GetAllAsync(query => query);

                var actorsList = _mapper.Map<IList<ActorListModel>>(actors);

                return actorsList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                throw new BaseException($"Failed to get all actors",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActorModel> GetAsync(int actorId)
        {
            var actor = await _actorRepository.GetAsync(query => query
                .Where(actor => actor.Id == actorId));

            if (actor == null)
                throw new BaseException($"Cinema with id {actorId} not found!", ExceptionType.ServerError,
                    HttpStatusCode.NotFound);

            var actorModel = _mapper.Map<ActorModel>(actor);

            return actorModel;
        }
        public async Task<ActorModel> Create(ActorCreateModel actorCreateModel)
        {
            try
            {
                var actor = PrepareActor(actorCreateModel);

                await _actorRepository.InsertAsync(actor);

                var actorModel = _mapper.Map<ActorModel>(actor);

                return actorModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating new actor, with exception message: {Message}", e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to create actor", ExceptionType.ServerError,
                    HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActorModel> Update(int actorId, ActorCreateModel actorUpdateModel)
        {
            try
            {
                var actor = await _actorRepository.GetAsync(query => query
                    .Where(actor => actor.Id == actorId));

                if (actor == null)
                    throw new BaseException($"Cinema with id: {actorId} does not exist",
                        ExceptionType.NotFound, HttpStatusCode.NotFound);

                _mapper.Map(actorUpdateModel, actor);

                _actorRepository.Update(actor);

                var actorModel = _mapper.Map<ActorModel>(actor);

                return actorModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to update cinema with id: {actorId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActorModel> Delete(int actorId)
        {
            try
            {
                var actor = await _actorRepository.GetAsync(query => query
                    .Where(actor => actor.Id == actorId));

                _actorRepository.Delete(actor);

                return _mapper.Map<ActorModel>(actor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                if (e is BaseException) throw;
                throw new BaseException($"Failed to delete actor with id: {actorId}!",
                    ExceptionType.ServerError, HttpStatusCode.InternalServerError);
            }
        }
        private Actor PrepareActor(ActorCreateModel actorCreateModel)
        {
            var actor = _mapper.Map<Actor>(actorCreateModel);
            //add authContextService to get user tracing later!
            actor.InsertDate = DateTime.UtcNow;
            actor.UpdateDate = DateTime.UtcNow;
            return actor;
        }




    }
}
