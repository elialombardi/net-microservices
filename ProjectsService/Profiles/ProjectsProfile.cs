using AutoMapper;
using ProjectsService.Dtos;
using ProjectsService.Models;

namespace ProjectsService.Profiles
{
  public class ProjectsProfile : Profile
  {
    public ProjectsProfile()
    {
      CreateMap<Project, ProjectRead>();
      CreateMap<ProjectCreate, Project>();
    }
  }
}