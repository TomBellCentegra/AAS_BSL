using AAS_BSL.Domain.Canonical.Transaction;
using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class CustomerProfile : Profile, IAutoMapperProfile
{
    public CustomerProfile()
    {
        CreateMap<Customer, Domain.Entyties.Transaction.Customer.Customer>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(u => u.email))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(u => u.customerType))
            .ForMember(dest => dest.CustomerID, opt => opt.MapFrom(u => u.id))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(u => u.phoneNumber))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(u => u.birthdate));
    }
}