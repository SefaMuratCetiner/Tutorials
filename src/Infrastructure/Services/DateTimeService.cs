using Tutorials.Application.Common.Interfaces;

namespace Tutorials.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
