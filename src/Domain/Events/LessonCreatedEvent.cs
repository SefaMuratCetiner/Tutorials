namespace Tutorials.Domain.Events;

public record LessonCreatedEvent(Lesson lesson) : BaseEvent;
