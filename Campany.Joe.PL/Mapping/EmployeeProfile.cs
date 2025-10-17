using AutoMapper;
using Campany.Joe.PL.Dtos;
using Company.DAL.Models;

namespace Campany.Joe.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDtos, Employee>().ReverseMap();
            //CreateMap<Employee, CreateEmployeeDtos>();
        }
    }
}
