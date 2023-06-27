using AAS_BSL.Domain.Canonical.Transaction;
using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class TotalsProfile : Profile, IAutoMapperProfile
{
    public TotalsProfile()
    {
        CreateMap<Totals, Domain.Entyties.Payment.Totals>()
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(u => u.discountAmount.amount))
            .ForMember(dest => dest.GrandAmount, opt => opt.MapFrom(u => u.grandAmount.amount))
            .ForMember(dest => dest.VoidsAmount, opt => opt.MapFrom(u => u.voidsAmount.amount))
            .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(u => u.netAmount.amount))
            .ForMember(dest => dest.TaxExclusive, opt => opt.MapFrom(u => u.taxExclusive.amount))
            .ForMember(dest => dest.GrossAmount, opt => opt.MapFrom(u => u.grossAmount.amount));
    }
}