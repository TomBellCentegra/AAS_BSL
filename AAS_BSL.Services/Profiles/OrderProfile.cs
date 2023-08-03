using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class OrderProfile : Profile, IAutoMapperProfile
{
    public OrderProfile()
    {
        CreateMap<Domain.Canonical.Transaction.Order, Domain.Entyties.Transaction.Order.Order>()
            .ForMember(dest => dest.Channel, opt => opt.MapFrom(u => u.orderChannel))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(u => u.orderNumber))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(u => u.orderSource))
            .ForMember(dest => dest.ReferenceId, opt => opt.MapFrom(u => u.referenceId))
            .ForMember(dest => dest.ModeId, opt => opt.MapFrom(u => u.orderMode != null ? u.orderMode.id : null))
            .ForMember(dest => dest.ModeName, opt => opt.MapFrom(u => u.orderMode != null ? u.orderMode.name : null));
    }
}