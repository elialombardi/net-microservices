using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProjectsService.Models;
using ProjectsService.Repos;

namespace ProjectsService.Data
{
    public static class DbSeede
    {
        public static async Task Seed(IApplicationBuilder app, bool isProd)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var projectsRepo = serviceScope.ServiceProvider.GetService<ProjectsRepo>();

            var projects = new List<Project>() {
                new() {
                    Name = "Ecommerce",
                    Description = "This is the first project I work on!",
                    Price = 100,
                },

                new() {
                    Name = "Slack clone",
                    Description = "My second project",
                    Price = 50.75M
                }
            };

            if (await projectsRepo.Count() == 0)
            {
                await projectsRepo.CreateMany(projects);
            }

        }
    }
}