using AAS_BSL.Domain.Canonical.Transaction;
using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class TaxProfile : Profile, IAutoMapperProfile
{
    public TaxProfile()
    {
        CreateMap<Tax, AAS_BSL.Domain.Entyties.Item.Tax.Tax>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(u => u.amount.amount))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(u => u.name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(u => u.taxType))
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(u => Convert.ToInt32(u.id)))
            .ForMember(dest => dest.TaxableAmount,
                opt => opt.MapFrom(u => u.taxableAmount != null ? u.taxableAmount.amount : 0));
    }
}