using AAS_BSL.Domain.Canonical.Transaction;
using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class EmployeeProfile : Profile, IAutoMapperProfile
{
    public EmployeeProfile()
    {
        CreateMap<Person, Domain.Entyties.Transaction.Emploee.Employee>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.name))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(u => u.roleId))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(u => u.roleName))
            .ForMember(dest => dest.ShiftId, opt => opt.MapFrom(u => u.shiftId))
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(u => u.id));
    }
}