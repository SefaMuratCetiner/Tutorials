using Microsoft.AspNetCore.Mvc;
using Tutorials.Application.Common.Dtos;
using Tutorials.Application.Common.Models;
using Tutorials.Application.Lessons.Commands.CreateLesson;
using Tutorials.Application.Lessons.Commands.DeleteLesson;
using Tutorials.Application.Lessons.Commands.UpdateLesson;
using Tutorials.Application.Lessons.Queries.GetLessonsWithPagination;

namespace Tutorials.WebApi.Controllers;

public class LessonsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<LessonDto>>> GetLessonsWithPagination([FromQuery] GetLessonsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateLessonCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateLessonCommand command)
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
        await Mediator.Send(new DeleteLessonCommand(id));

        return NoContent();
    }
}
