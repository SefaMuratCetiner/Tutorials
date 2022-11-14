namespace Tutorials.Domain.Entities;

public class Lesson : BaseAuditableEntity
{
    public int ProgrammeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }
}
