using Tutorials.Application.Common.Exceptions;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;
using MediatR;

namespace Tutorials.Application.Lessons.Commands.UpdateLesson;

public record UpdateLessonCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; set; }

    public string? Link { get; set; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateLessonCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Lessons
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Lesson), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Link = request.Link;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
