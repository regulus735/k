using AutoMapper;

namespace Kolokwium.Services.Configuration.AutoMapperProfiles;
public class MainProfile : Profile
{
    public MainProfile()
    {
        //AutoMapper maps
      CreateMap<Group, GroupVm>()
        .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students))
        .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.SubjectGroups.Select(s => s.Subject)));

        CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

        CreateMap<Student, StudentVm>()
        .ForMember(dest => dest.GroupName, x => x.MapFrom(src => src.Group == null ? null : src.Group.Name))
        .ForMember(dest => dest.ParentName,
        x => x.MapFrom(src => src.Parent == null ? null : $"{src.Parent.FirstName} {src.Parent.LastName}"));
    }
}

