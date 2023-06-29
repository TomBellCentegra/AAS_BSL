using AAS_BSL.Domain.Entyties.Item.Tax;
using AAS_BSL.Domain.Entyties.Payment;
using AAS_BSL.Domain.Entyties.Transaction.Customer;
using AAS_BSL.Domain.Entyties.Transaction.Discount;
using AAS_BSL.Domain.Entyties.Transaction.Emploee;
using AAS_BSL.Domain.Entyties.Transaction.Order;
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

    #region Discount

    public static Domain.Canonical.Transaction.Discount ToModel(this Discount entity)
    {
        return entity.MapTo<Discount, Domain.Canonical.Transaction.Discount>();
    }

    public static Discount ToEntity(this Domain.Canonical.Transaction.Discount model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Discount, Discount>();
    }

    public static Discount ToEntity(this Domain.Canonical.Transaction.Discount model, Discount destination)
    {
        return model.MapTo(destination);
    }

    #endregion

    #region Totals

    public static Domain.Canonical.Transaction.Totals ToModel(this Totals entity)
    {
        return entity.MapTo<Totals, Domain.Canonical.Transaction.Totals>();
    }

    public static Totals ToEntity(this Domain.Canonical.Transaction.Totals model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Totals, Totals>();
    }

    public static Totals ToEntity(this Domain.Canonical.Transaction.Totals model, Totals destination)
    {
        return model.MapTo(destination);
    }

    #endregion

    #region Customer

    public static Domain.Canonical.Transaction.Customer ToModel(this Customer entity)
    {
        return entity.MapTo<Customer, Domain.Canonical.Transaction.Customer>();
    }

    public static Customer ToEntity(this Domain.Canonical.Transaction.Customer model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Customer, Customer>();
    }

    public static Customer ToEntity(this Domain.Canonical.Transaction.Customer model, Customer destination)
    {
        return model.MapTo(destination);
    }

    #endregion

    #region Employee

    public static Domain.Canonical.Transaction.Person ToModel(this Employee entity)
    {
        return entity.MapTo<Employee, Domain.Canonical.Transaction.Person>();
    }

    public static Employee ToEntity(this Domain.Canonical.Transaction.Person model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Person, Employee>();
    }

    public static Employee ToEntity(this Domain.Canonical.Transaction.Person model, Employee destination)
    {
        return model.MapTo(destination);
    }

    #endregion

    #region Order

    public static Domain.Canonical.Transaction.Order ToModel(this Order entity)
    {
        return entity.MapTo<Order, Domain.Canonical.Transaction.Order>();
    }

    public static Order ToEntity(this Domain.Canonical.Transaction.Order model)
    {
        return model.MapTo<Domain.Canonical.Transaction.Order, Order>();
    }

    public static Order ToEntity(this Domain.Canonical.Transaction.Order model, Order destination)
    {
        return model.MapTo(destination);
    }

    #endregion
}