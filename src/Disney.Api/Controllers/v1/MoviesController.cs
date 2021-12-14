using Application.Features.Movies.Commands.CreateMovieCommand;
using Application.Features.Movies.Commands.DeleteMovieCommand;
using Application.Features.Movies.Commands.UpdateMovieCommand;
using Application.Features.Movies.Queries.GetAllMoviesQuery;
using Application.Features.Movies.Queries.GetMovieByIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disney.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class MoviesController : BaseApiController
    {
        //// GET api/v1/[controller]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMoviesParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllMoviesQuery
            {
                Title = filter.Title,
                Gender = filter.Gender
            }));
        }

        // GET api/v1/[controller]/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetMovieByIdQuery { MovieId = id }));
        }

        // POST api/v1/[controller]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMovieCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateMovieCommand command)
        {
            if (id != command.MovieId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/v1/[controller]/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMovieCommand { MovieId = id }));
        }
    }
}
