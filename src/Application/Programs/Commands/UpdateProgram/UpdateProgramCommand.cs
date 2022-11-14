using MediatR;
using Tutorials.Application.Common.Exceptions;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;

namespace Tutorials.Application.Programs.Commands.UpdateProgram;

public record UpdateProgramCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsPublished { get; set; }
}

public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProgramCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Programmes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Programme), request.Id);
        }

        entity.Title = request.Title;
        entity.Start = request.Start;
        entity.End  = request.End;
        entity.IsPublished = request.IsPublished;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
