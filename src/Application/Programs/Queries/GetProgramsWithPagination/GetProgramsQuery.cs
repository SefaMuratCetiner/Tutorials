using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorials.Application.Common.Dtos;
using Tutorials.Application.Common.Interfaces;
using Tutorials.Application.Common.Mappings;
using Tutorials.Application.Common.Models;

namespace Tutorials.Application.Programs.Queries.GetProgramsWithPagination;

public record GetProgramsWithPaginationQuery : IRequest<PaginatedList<ProgrammeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProgramsWithPaginationQueryHandler : IRequestHandler<GetProgramsWithPaginationQuery, PaginatedList<ProgrammeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProgramsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProgrammeDto>> Handle(GetProgramsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Programmes
                .AsNoTracking()
                .OrderBy(t => t.Title)
                .ProjectTo<ProgrammeDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
