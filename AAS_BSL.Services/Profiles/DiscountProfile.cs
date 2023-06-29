using AAS_BSL.Domain.Canonical.Transaction;
using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class DiscountProfile : Profile, IAutoMapperProfile
{
    public DiscountProfile()
    {
        CreateMap<Discount, Domain.Entyties.Transaction.Discount.Discount>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(u => u.amount.amount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(u => u.discountType));
    }
}