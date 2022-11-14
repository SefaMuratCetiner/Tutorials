using FluentValidation;

namespace Tutorials.Application.Lessons.Queries.GetLessonsWithPagination;

public class GetLessonsWithPaginationQueryValidator : AbstractValidator<GetLessonsWithPaginationQuery>
{
    public GetLessonsWithPaginationQueryValidator()
    {
        RuleFor(x => x.ProgrammeId)
            .NotEmpty().WithMessage("ProgrammeId is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be at least greater than or equal to 1.");
    }
}
