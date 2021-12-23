using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TodoService.Models;

namespace TodoService.Data
{
  public static class PrepDb
  {
    public static async Task PrepPopulation(IApplicationBuilder app)
    {
      using var serviceScope = app.ApplicationServices.CreateScope();
      await SeedData(serviceScope.ServiceProvider.GetService<TodoDbContext>());
    }
    private static async Task SeedData(TodoDbContext context)
    {
      if (!context.TodoItems.Any())
      {
        await context.TodoItems.AddRangeAsync(
          new() { Title = "Do laundy", CreatedOn = DateTime.UtcNow },
        new() { Title = "Start new project", Description = "Brainstorm ideas", CreatedOn = DateTime.UtcNow },
        new() { Title = "Test the camera", CreatedOn = DateTime.UtcNow, DeletedOn = DateTime.UtcNow.AddHours(2) },
        new() { Title = "Take the dog out", CreatedOn = DateTime.UtcNow, CompletedOn = DateTime.UtcNow.AddDays(1) });

        await context.SaveChangesAsync();
      }
      else
      {
        Console.WriteLine("Data alreayd present. Not seeding again.");
      }
    }
  }
}