namespace AAS_BSL.Services.Item;

public interface IItemRepository
{
    Task Add(Domain.Entyties.Item.Item item);
    Task BatchAdd(IEnumerable<Domain.Entyties.Item.Item> items);
    Task BatchDelete(IEnumerable<Domain.Entyties.Item.Item> items);
    Task BatchUpdate(IEnumerable<Domain.Entyties.Item.Item> items, IEnumerable<Domain.Entyties.Item.Item> currentItems);
}