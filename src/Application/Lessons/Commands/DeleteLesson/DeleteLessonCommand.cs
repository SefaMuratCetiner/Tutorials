using Tutorials.Application.Common.Exceptions;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;
using Tutorials.Domain.Events;
using MediatR;

namespace Tutorials.Application.Lessons.Commands.DeleteLesson;

public record DeleteLessonCommand(int Id) : IRequest;

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLessonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Lessons
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Lesson), request.Id);
        }

        _context.Lessons.Remove(entity);

        entity.AddDomainEvent(new LessonDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
