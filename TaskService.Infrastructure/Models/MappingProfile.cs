using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Models;
using TaskService.Infrastructure.Models.Task;

namespace TaskService.Infrastructure.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskModel, TaskModelDTO>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.TaskName, opt => opt.MapFrom(c => c.TaskName))
                .ForMember(x => x.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(x => x.IsComplete, opt => opt.MapFrom(c => c.IsComplete))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => c.IsActive))
                .ForMember(x => x.CompletedTimeStamp, opt => opt.MapFrom(c => c.CompletedTimeStamp))
                .ForMember(x => x.UpdateTimeStamp, opt => opt.MapFrom(c => c.UpdateTimeStamp))
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(c => Encoding.ASCII.GetString(c.RowVersion)));

            CreateMap<TaskModelDTO, TaskModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.TaskName, opt => opt.MapFrom(c => c.TaskName))
                .ForMember(x => x.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember(x => x.IsComplete, opt => opt.MapFrom(c => c.IsComplete))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(c => c.IsActive))
                .ForMember(x => x.CompletedTimeStamp, opt => opt.MapFrom(c => c.CompletedTimeStamp))
                .ForMember(x => x.UpdateTimeStamp, opt => opt.MapFrom(c => c.UpdateTimeStamp))
                .ForMember(x => x.RowVersion, opt => opt.MapFrom(c => Encoding.ASCII.GetBytes(c.RowVersion)));
        }
    }
}
