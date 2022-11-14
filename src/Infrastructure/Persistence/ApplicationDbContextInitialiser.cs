using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tutorials.Domain.Entities;

namespace Tutorials.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Programmes.Any())
        {
            _context.Programmes.Add(new Programme
            {
                Title = "Program 1",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(5),
                IsPublished = true,
                Lessons =
                {
                    new Lesson { Title = "Lesson 1", Description = "Description for Lesson 1", Link = "Link for Lesson 1" },
                    new Lesson { Title = "Lesson 2", Description = "Description for Lesson 2", Link = "Link for Lesson 2" },
                    new Lesson { Title = "Lesson 3", Description = "Description for Lesson 3", Link = "Link for Lesson 3" },
                    new Lesson { Title = "Lesson 4", Description = "Description for Lesson 4", Link = "Link for Lesson 4" },
                }
            });

            _context.Programmes.Add(new Programme
            {
                Title = "Program 2",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(10),
                IsPublished = true,
                Lessons =
                {
                    new Lesson { Title = "Lesson 5", Description = "Description for Lesson 5", Link = "Link for Lesson 5" },
                    new Lesson { Title = "Lesson 6", Description = "Description for Lesson 6", Link = "Link for Lesson 6" },
                    new Lesson { Title = "Lesson 7", Description = "Description for Lesson 7", Link = "Link for Lesson 7" },
                }
            });

            _context.Programmes.Add(new Programme
            {
                Title = "Program 3",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(15),
                IsPublished = true,
                Lessons =
                {
                    new Lesson { Title = "Lesson 8", Description = "Description for Lesson 8", Link = "Link for Lesson 8" },
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
