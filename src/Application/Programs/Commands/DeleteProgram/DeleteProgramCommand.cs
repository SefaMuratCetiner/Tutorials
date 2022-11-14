using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorials.Application.Common.Exceptions;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Domain.Entities;

namespace Tutorials.Application.Programs.Commands.DeleteProgram;

public record DeleteProgramCommand(int Id) : IRequest;

public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProgramCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Programmes
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Programme), request.Id);
        }

        _context.Programmes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
