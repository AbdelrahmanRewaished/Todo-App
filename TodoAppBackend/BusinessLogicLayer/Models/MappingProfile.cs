using AutoMapper;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<TodoDto, Todo>().ReverseMap();
            CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
        }
    }
}
