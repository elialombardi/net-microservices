using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectsService.Data;
using ProjectsService.Dtos;
using ProjectsService.Models;

namespace ProjectsService.Controllers
{
    [Route("api/[controller]"
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemRepo todoItemRepo;
        private readonly IMapper mapper;

        public ProjectController(IProjectRepo ProjectRepo, IMapper mapper)
        {
            this.ProjectRepo = ProjectRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemRead>>> GetAll()
        {
            return Ok(mapper.Map<TodoItemRead[]>(await todoItemRepo.GetAll()));
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<ActionResult<TodoItemRead>> GetById(int id)
        {
            var item = mapper.Map<TodoItemRead>(await todoItemRepo.GetOne(id));

            return item != null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemRead>> Create(TodoItemCreate data)
        {
            var item = await todoItemRepo.Create(mapper.Map<TodoItem>(data));

            return CreatedAtRoute(nameof(GetById), new { Id = item.Id }, mapper.Map<TodoItemRead>(item));
        }
    }
}