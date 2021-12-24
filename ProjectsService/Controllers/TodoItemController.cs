using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsService.Controllers
{
  [Route("api/p/todo-items")]
  [ApiController]
  public class TodoItemController : ControllerBase
  {

    public TodoItemController()
    {
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound connection successfull");
      return Ok("Inbound connection successfull");
    }
  }
}