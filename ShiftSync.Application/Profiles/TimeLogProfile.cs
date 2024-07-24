using AutoMapper;
using ShiftSync.Application.Dtos;
using ShiftSync.Core.Entities;


namespace ShiftSync.Application.Profiles
{
    public class TimeLogProfile : Profile
    {
        public TimeLogProfile()
        {
            CreateMap<CreateTimeLogDto, TimeLog>();
            CreateMap<TimeLog, ReadTimeLogDto>();
        }
    }
}