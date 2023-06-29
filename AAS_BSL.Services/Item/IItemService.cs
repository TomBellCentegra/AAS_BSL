namespace AAS_BSL.Services.Item;

public interface IItemService
{
    Task AddList(IEnumerable<Domain.Entyties.Item.Item> items);
}