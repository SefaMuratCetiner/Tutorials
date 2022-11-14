using MediatR;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;

namespace Tutorials.Application.Programs.Commands.CreateProgram;

public record CreateProgramCommand : IRequest<int>
{
    public string? Title { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsPublished { get; set; }
}

public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProgramCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = new Programme
        {
            Title = request.Title,
            Start = request.Start,
            End = request.End,
            IsPublished = request.IsPublished
        };

        _context.Programmes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
