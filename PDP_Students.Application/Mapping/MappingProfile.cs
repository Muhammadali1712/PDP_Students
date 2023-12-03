using AutoMapper;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models.MentorDTO;
using PDP_Students.Domain.Models.StudentDTO;

namespace PDP_Students.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StudentCreateDTO, Student>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleId
            .Select(x => new Role()
            {
                Id = x
            }).ToList()));

        CreateMap<Student, StudentGetDTO>();/*
            ForMember(dest => dest.MentorId, opt => opt.MapFrom(src => src.Mentor.Id));*/

        CreateMap<StudentUpdateDTO, Student>();

        CreateMap<MentorCreateDTO, Mentor>();
        /* ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentsId
         .Select(x => new Student
         {
             Id = x
         })));*/

        CreateMap<Mentor, MentorGetDTO>();
        /* .ForMember(dest => dest.StudentsId, opt => opt.MapFrom(src => src.Students.
         Select(x => x.Id)));*/

        CreateMap<MentorUpdateDTO, Mentor>();
        /*  ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentsId
          .Select(x => new Student
          {
              Id = x
          })));*/
    }
}
