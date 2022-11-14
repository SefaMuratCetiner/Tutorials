using Tutorials.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tutorials.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<Programme> Programmes { get; }

    DbSet<Lesson> Lessons { get; }
}
