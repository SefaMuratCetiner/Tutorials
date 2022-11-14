using FluentValidation;
using MediatR;
using System.Reflection;
using Tutorials.Application.Common.Behaviours;
using Tutorials.Application.Lessons.Commands.CreateLesson;
using Tutorials.Application.Lessons.Commands.UpdateLesson;
using Tutorials.Application.Lessons.Queries.GetLessonsWithPagination;
using Tutorials.Application.Programs.Commands.CreateProgram;
using Tutorials.Application.Programs.Commands.UpdateProgram;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IValidator<CreateLessonCommand>, CreateLessonCommandValidator>();
        services.AddScoped<IValidator<UpdateLessonCommand>, UpdateLessonCommandValidator>();
        services.AddScoped<IValidator<GetLessonsWithPaginationQuery>, GetLessonsWithPaginationQueryValidator>();
        services.AddScoped<IValidator<CreateProgramCommand>, CreateProgramCommandValidator>();
        services.AddScoped<IValidator<UpdateProgramCommand>, UpdateProgramCommandValidator>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
