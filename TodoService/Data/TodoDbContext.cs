using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> opt) : base(opt)
        {
        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}