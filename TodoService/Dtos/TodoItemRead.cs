using System;

namespace TodoService.Dtos
{
  public class TodoItemRead
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? CompletedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
  }
}