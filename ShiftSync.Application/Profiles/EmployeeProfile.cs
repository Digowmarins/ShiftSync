using AutoMapper;
using ShiftSync.Application.Dtos;
using ShiftSync.Core.Entities;


namespace ShiftSync.Application.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<Employee, ReadEmployeeDto>();
        }
    }
}