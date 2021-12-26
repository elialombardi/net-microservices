using System;

namespace TodoService.Dtos
{
    public class TodoItemCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}