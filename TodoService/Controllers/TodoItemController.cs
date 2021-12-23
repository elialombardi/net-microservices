using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoService.Data;
using TodoService.Dtos;
using TodoService.Models;

namespace TodoService.Controllers
{
  [Route("api/todo-items")]
  [ApiController]
  public class TodoItemController : ControllerBase
  {
    private readonly ITodoItemRepo todoItemRepo;
    private readonly IMapper mapper;

    public TodoItemController(ITodoItemRepo todoItemRepo, IMapper mapper)
    {
      this.todoItemRepo = todoItemRepo;
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