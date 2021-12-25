using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectsService.Models;
using ProjectsService.Repos;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectsRepo projectsRepo;

        public ProjectController(IProjectsRepo projectsRepo)
        {
            this.projectsRepo = projectsRepo;
        }

        [HttpGet]
        public ActionResult<List<Project>> Get() =>
            projectsRepo.Get();

        [HttpGet("{id:length(24)}", Name = "GetProject")]
        public ActionResult<Project> Get(string id)
        {
            var Project = projectsRepo.Get(id);

            if (Project == null)
            {
                return NotFound();
            }

            return Project;
        }

        [HttpPost]
        public ActionResult<Project> Create(Project Project)
        {
            projectsRepo.Create(Project);

            return CreatedAtRoute("GetProject", new { id = Project.Id.ToString() }, Project);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Project ProjectIn)
        {
            var Project = projectsRepo.Get(id);

            if (Project == null)
            {
                return NotFound();
            }

            projectsRepo.Update(id, ProjectIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var Project = projectsRepo.Get(id);

            if (Project == null)
            {
                return NotFound();
            }

            projectsRepo.Remove(Project.Id);

            return NoContent();
        }

    }
}