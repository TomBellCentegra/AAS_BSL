using AAS_BSL.Domain.Entyties.Item.Tax;
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

    #region Taxes

    public static Domain.Canonical.Transaction.Tax ToModel(this Tax entity)
    {
        return entity.MapTo<Tax, Domain.Canonical.Transaction.Tax>();
    }

    public static Tax ToEntity(this Domain.Canonical.Transaction.Tax model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Tax, Tax>();
    }

    public static Tax ToEntity(this Domain.Canonical.Transaction.Tax model, Tax destination)
    {
        return model.MapTo(destination);
    }

    #endregion
}