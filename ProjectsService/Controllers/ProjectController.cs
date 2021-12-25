using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectsService.Dtos;
using ProjectsService.Models;
using ProjectsService.Repos;

namespace ProjectsService.Controllers
{
  [Route("api/projects")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly IProjectsRepo projectsRepo;
    private readonly IMapper mapper;

    public ProjectController(IProjectsRepo projectsRepo, IMapper mapper)
    {
      this.projectsRepo = projectsRepo;
      this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult<List<Project>> Get() =>
        projectsRepo.Get();

    [HttpGet("{id:length(24)}", Name = "GetProject")]
    public ActionResult<ProjectRead> Get(string id)
    {
      var Project = projectsRepo.Get(id);

      if (Project == null)
      {
        return NotFound();
      }

      return mapper.Map<ProjectRead>(Project);
    }

    [HttpPost]
    public ActionResult<ProjectRead> Create(ProjectCreate data)
    {
      var project = projectsRepo.Create(mapper.Map<Project>(data));

      return CreatedAtRoute("GetProject", new { id = project.Id.ToString() }, mapper.Map<ProjectRead>(project));
    }

    // [HttpPut("{id:length(24)}")]
    // public IActionResult Update(string id, Project ProjectIn)
    // {
    //   var Project = projectsRepo.Get(id);

    //   if (Project == null)
    //   {
    //     return NotFound();
    //   }

    //   projectsRepo.Update(id, ProjectIn);

    //   return NoContent();
    // }

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