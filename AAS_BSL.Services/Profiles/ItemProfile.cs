using AutoMapper;

namespace AAS_BSL.Services.Profiles;

public class ItemProfile : Profile, IAutoMapperProfile
{
    public ItemProfile()
    {
        CreateMap<Domain.Canonical.Transaction.Item, Domain.Entyties.Item.Item>()
            .ForMember(dest => dest.ItemID, opt => opt.MapFrom(u => u.id))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(u => u.itemDiscounts.Sum(x => x.amount.amount)))
            .ForMember(dest => dest.Measurement, opt => opt.MapFrom(u => u.quantity.unitOfMeasurement))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(u => u.quantity.quantity))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(u => u.productId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(u => u.productName))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(u => u.actualUnitPrice.amount))
            .ForMember(dest => dest.ParentItemId, opt => opt.MapFrom(u => u.parentItemId))
            .ForMember(dest => dest.Taxes, opt => opt.MapFrom(u => u.itemTaxes))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(u => u.actualUnitPrice.amount));
    }
}