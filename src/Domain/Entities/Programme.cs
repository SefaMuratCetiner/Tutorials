namespace Tutorials.Domain.Entities;

public class Programme : BaseAuditableEntity
{
    public string? Title { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public bool IsPublished { get; set; }

    public IList<Lesson> Lessons { get; private set; } = new List<Lesson>();
}
