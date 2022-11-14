using Microsoft.AspNetCore.Mvc;
using Tutorials.Application.Common.Dtos;
using Tutorials.Application.Common.Models;
using Tutorials.Application.Programs.Commands.CreateProgram;
using Tutorials.Application.Programs.Commands.DeleteProgram;
using Tutorials.Application.Programs.Commands.UpdateProgram;
using Tutorials.Application.Programs.Queries.GetProgramsWithPagination;

namespace Tutorials.WebApi.Controllers;

public class ProgramsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProgrammeDto>>> GetProgramsWithPagination([FromQuery] GetProgramsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProgramCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateProgramCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteProgramCommand(id));

        return NoContent();
    }
}
