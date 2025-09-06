using AutoMapper;
using ToDoAPI.Bll.Features.Tasks.Commands.CreateTask;
using ToDoAPI.Bll.Features.Tasks.Commands.UpdateTask;
using ToDoAPI.Bll.Features.Tasks.DTOs;
using ToDoAPI.Dal.Entities;

namespace ToDoAPI.Bll.Features.Tasks.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
            CreateMap<CreateTaskCommand, TaskEntity>().ReverseMap();
            CreateMap<UpdateTaskCommand, TaskEntity>().ReverseMap();
        }
    }
}