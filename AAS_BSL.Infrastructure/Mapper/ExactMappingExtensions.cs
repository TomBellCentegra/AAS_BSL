using Item = AAS_BSL.Domain.Entyties.Item.Item;

namespace AAS_BSL.Infrastructure.Mapper;

public static class ExactMappingExtensions
{
    #region Items

    public static AAS_BSL.Domain.Canonical.Transaction.Item ToModel(this Item entity)
    {
        return entity.MapTo<Item, AAS_BSL.Domain.Canonical.Transaction.Item>();
    }

    public static Item ToEntity(this AAS_BSL.Domain.Canonical.Transaction.Item model)
    {
        return model.MapTo<AAS_BSL.Domain.Canonical.Transaction.Item, Item>();
    }

    public static Item ToEntity(this AAS_BSL.Domain.Canonical.Transaction.Item model, Item destination)
    {
        return model.MapTo(destination);
    }

    #endregion
}