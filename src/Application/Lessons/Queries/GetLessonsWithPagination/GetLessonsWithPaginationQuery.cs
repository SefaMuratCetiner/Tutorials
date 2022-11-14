using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Tutorials.Application.Common.Dtos;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Application.Common.Mappings;
using Tutorials.Application.Common.Models;

namespace Tutorials.Application.Lessons.Queries.GetLessonsWithPagination;

public record GetLessonsWithPaginationQuery : IRequest<PaginatedList<LessonDto>>
{
    public int ProgrammeId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetLessonsWithPaginationQueryHandler : IRequestHandler<GetLessonsWithPaginationQuery, PaginatedList<LessonDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLessonsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LessonDto>> Handle(GetLessonsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Lessons
            .Where(x => x.ProgrammeId == request.ProgrammeId)
            .OrderBy(x => x.Title)
            .ProjectTo<LessonDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
