using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectsService.Data;
using ProjectsService.Dtos;
using ProjectsService.Models;

namespace ProjectsService.Controllers
{
    [Route("api/p/todo-items")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {

        public TodoItemController()
        {
        }

        [HttpGet]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound connection successfull");
            return Ok("Inbound connection successfull");
        }
    }
}