using Application.Features.Characters.Commands.CreateCharacterCommand;
using Application.Features.Characters.Commands.DeleteCharacterCommand;
using Application.Features.Characters.Commands.UpdateCharacterCommand;
using Application.Features.Characters.Queries.GetAllCharactersQuery;
using Application.Features.Characters.Queries.GetCharacterByIdQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Disney.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class CharactersController : BaseApiController
    {
        //// GET api/v1/[controller]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCharactersParameters filter)
        {
            return Ok(await Mediator.Send(new GetAllCharactersQuery
            {
                Name = filter.Name,
                Age = filter.Age
            }));
        }

        // GET api/v1/[controller]/5
        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCharacterByIdQuery { CharacterId = id }));
        }

        // POST api/v1/[controller]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCharacterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCharacterCommand command)
        {
            if (id != command.CharacterId)
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
            return Ok(await Mediator.Send(new DeleteCharacterCommand { CharacterId = id }));
        }
    }
}
