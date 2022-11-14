using Tutorials.Application.Common.Mappings;
using Tutorials.Domain.Entities;

namespace Tutorials.Application.Common.Dtos
{
    public class ProgrammeDto : IMapFrom<Programme>
    {
        public string? Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsPublished { get; set; }

        public IList<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
}
