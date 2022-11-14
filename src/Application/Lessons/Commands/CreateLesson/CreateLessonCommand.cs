using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;
using Tutorials.Domain.Events;
using MediatR;

namespace Tutorials.Application.Lessons.Commands.CreateLesson;

public record CreateLessonCommand : IRequest<int>
{
    public int ProgrammeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }
}

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateLessonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var entity = new Lesson
        {
            ProgrammeId = request.ProgrammeId,
            Title = request.Title,
            Description = request.Description,
            Link = request.Link
        };

        entity.AddDomainEvent(new LessonCreatedEvent(entity));

        _context.Lessons.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
