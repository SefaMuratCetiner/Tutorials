using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Tutorials.Application.Common.Interfaces;

namespace Tutorials.Application.Programs.Commands.UpdateProgram;

public class UpdateProgramCommandValidator : AbstractValidator<UpdateProgramCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProgramCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateProgramCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.Programmes
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
