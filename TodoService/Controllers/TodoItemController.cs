using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoService.AsyncDataServices;
using TodoService.Data;
using TodoService.Dtos;
using TodoService.Models;
using TodoServuce.SyncDataServices.Http;

namespace TodoService.Controllers
{
    [Route("api/todo-items")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemRepo todoItemRepo;
        private readonly IMapper mapper;
        private readonly IHttpProjectsDataClient httpProjectsDataClient;
        private readonly IMessageBusClient messageBusClient;

        public TodoItemController(ITodoItemRepo todoItemRepo,
                                  IMapper mapper,
                                  IHttpProjectsDataClient httpProjectsDataClient,
                                  IMessageBusClient messageBusClient)
        {
            this.todoItemRepo = todoItemRepo;
            this.mapper = mapper;
            this.httpProjectsDataClient = httpProjectsDataClient;
            this.messageBusClient = messageBusClient;
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
            var itemRead = mapper.Map<TodoItemRead>(item);

            try
            {
                await httpProjectsDataClient.SendItemToProject(itemRead);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendItemToProject Error", ex);
            }

            try
            {
                var publishedDto = mapper.Map<TodoItemPublished>(itemRead);
                publishedDto.Event = "TodoItem_Published";

                messageBusClient.PublishTodoItem(publishedDto);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Couldn't send published todo item {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetById), new { Id = item.Id }, itemRead);
        }
    }
}