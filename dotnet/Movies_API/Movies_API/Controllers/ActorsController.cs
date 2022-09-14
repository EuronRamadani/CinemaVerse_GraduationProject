using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.Services.Models.Actors;
using Movies.Services.Models.Common;
using Movies.Services.Services.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    [Route("api/cinemas/{cinemaId}/movies/{movieId}/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly ILogger<ActorsController> _logger;

        public ActorsController(IActorService actorService, ILoggerFactory logger)
        {
            _actorService = actorService ?? throw new ArgumentNullException(nameof(actorService));
            _logger = logger.CreateLogger<ActorsController>() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<ActorListModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var actors = await _actorService.GetAllAsync();
            return Ok(new ApiResponse<IList<ActorListModel>>(actors));
        }

        [HttpGet]
        [Route("{actorId}")]
        [ProducesResponseType(typeof(ApiResponse<ActorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int actorId)
        {
            var actor = await _actorService.GetAsync(actorId);
            return Ok(new ApiResponse<ActorModel>(actor));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ActorModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ActorCreateModel actorModel)
        {
            var actor = await _actorService.Create(actorModel);
            return CreatedAtAction(
                nameof(Get),
                new { actorId = actor.Id },
                new ApiResponse<ActorModel>(actor)
            );
        }

        [HttpPut]
        [Route("{actorId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int actorId, [FromBody] ActorCreateModel actorModel)
        {
            var actor = await _actorService.Update(actorId, actorModel);
            return Ok(new ApiResponse<ActorModel>(actor));
        }

        [HttpDelete]
        [Route("{actorId}")]
        [ProducesResponseType(typeof(ApiResponse<ActorModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int actorId)
        {
            var actor = await _actorService.Delete(actorId);
            return Ok(new ApiResponse<ActorModel>(actor));
        }
    }
}
