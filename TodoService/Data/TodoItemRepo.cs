using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Data
{

  public class TodoItemRepo : ITodoItemRepo
  {
    private readonly TodoDbContext context;

    public TodoItemRepo(TodoDbContext context)
    {
      this.context = context;
    }
    public async Task<TodoItem> Create(TodoItem item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(TodoItem));

      context.TodoItems.Add(item);

      await context.SaveChangesAsync();

      return item;
    }

    public async Task<IEnumerable<TodoItem>> GetAll()
    {
      return await context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> GetOne(int id)
    {
      return await context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    }
  }
}