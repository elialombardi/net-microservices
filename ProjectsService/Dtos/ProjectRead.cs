using System;

namespace ProjectsService.Dtos
{
  public class ProjectRead
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
  }


}