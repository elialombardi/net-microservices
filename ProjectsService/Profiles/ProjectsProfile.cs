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
            CreateMap<TodoItem, TodoItemRead>();
            CreateMap<TodoItemPublished, TodoItem>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
        }
    }
}