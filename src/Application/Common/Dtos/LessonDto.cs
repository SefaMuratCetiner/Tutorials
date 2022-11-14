using Tutorials.Application.Common.Mappings;
using Tutorials.Domain.Entities;

namespace Tutorials.Application.Common.Dtos
{
    public class LessonDto : IMapFrom<Lesson>
    {
        public int ProgrammeId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Link { get; set; }
    }

}
