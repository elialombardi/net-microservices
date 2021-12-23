using AutoMapper;
using TodoService.Dtos;
using TodoService.Models;

namespace TodoService.Profiles
{
  public class TodoItemProfile : Profile
  {
    public TodoItemProfile()
    {
      CreateMap<TodoItem, TodoItemRead>();
      CreateMap<TodoItemCreate, TodoItem>();
    }
  }
}