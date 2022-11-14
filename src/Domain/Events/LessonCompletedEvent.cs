namespace Tutorials.Domain.Events;

public record LessonCompletedEvent(Lesson lesson) : BaseEvent;
