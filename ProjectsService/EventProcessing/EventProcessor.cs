using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProjectsService.Dtos;
using ProjectsService.Models;
using ProjectsService.Repos;

namespace ProjectService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        private string DetermineEvent(string notificationMessage)
        {
            Console.WriteLine($"Determining event {notificationMessage}");

            var eventType = JsonSerializer.Deserialize<GenericEvent>(notificationMessage);
            switch (eventType.Event)
            {
                case EventType.TodoItemPublished:
                    return EventType.TodoItemPublished;
                default:
                    return EventType.Undeterminated;
            }
        }

        private async Task AddTodoItem(string message)
        {
            using var scope = scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IProjectsRepo>();
            var data = JsonSerializer.Deserialize<TodoItemPublished>(message);
            var todoItem = mapper.Map<TodoItem>(data);
            try
            {
                if (await repo.ExternalTodoItemExists(todoItem.ExternalID))
                    Console.WriteLine($"External todo item exists");
                else
                    repo.CreateTodoItem(todoItem);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error adding published todo Item {ex.Message}");
                throw;
            }
        }

        public async Task ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.TodoItemPublished:
                    await AddTodoItem(message);
                    break;
                default:
                    Console.WriteLine($"Event not managed: {message}");
                    break;
            }
        }
    }
}