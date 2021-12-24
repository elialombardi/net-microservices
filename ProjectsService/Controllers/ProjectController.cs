using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {

    public ProjectController()
    {
    }

    [HttpGet]
    public ActionResult Get()
    {
      return Ok("ProjectController");
    }

  }
}