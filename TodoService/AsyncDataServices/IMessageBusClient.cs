using TodoService.Dtos;

namespace TodoService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishTodoItem(TodoItemPublished todoItemPublished);
    }
}