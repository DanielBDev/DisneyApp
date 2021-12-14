using Application.Features.Genders.Commands.CreateGenderCommand;
using Application.Features.Genders.Commands.DeleteGenderCommand;
using Application.Features.Genders.Commands.UpdateGenderCommand;
using Application.Features.Genders.Queries.GetAllGendersQuery;
using Application.Features.Genders.Queries.GetGenderByIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disney.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class GendersController : BaseApiController
    {
        //// GET api/v1/[controller]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllGendersParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllGendersQuery
            {
                Name = filter.Name
            }));
        }

        // GET api/v1/[controller]/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetGenderByIdQuery { GenderId = id }));
        }

        // POST api/v1/[controller]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGenderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateGenderCommand command)
        {
            if (id != command.GenderId)
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
            return Ok(await Mediator.Send(new DeleteGenderCommand { GenderId = id }));
        }
    }
}
