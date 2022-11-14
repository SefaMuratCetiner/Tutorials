namespace Tutorials.Domain.Events;

public record LessonDeletedEvent(Lesson Lesson) : BaseEvent;
