using System.Collections.Generic;
using System.Threading.Tasks;
using TodoService.Models;

namespace TodoService.Data
{
  public interface ITodoItemRepo
  {
    Task<TodoItem> Create(TodoItem item);
    Task<IEnumerable<TodoItem>> GetAll();
    Task<TodoItem> GetOne(int id);
  }
}