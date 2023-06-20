using AAS_BSL.Infrastructure.Mapper;
using AAS_BSL.Services.Item.Tax;

namespace AAS_BSL.Services.Item;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ITaxRepository _taxRepository;

    public ItemService(IItemRepository itemRepository, ITaxRepository taxRepository)
    {
        _itemRepository = itemRepository;
        _taxRepository = taxRepository;
    }

    public async Task AddList(IEnumerable<Domain.Entyties.Item.Item> items)
    {
        if (!items.Any())
        {
            return;
        }

        foreach (var item in items)
        {
            var itemId = await _itemRepository.Add(item);

            var taxes = item.Taxes.Select(x =>
            {
                x.ItemId = itemId;
                return x;
            });
            await _taxRepository.BatchAdd(taxes);
        }
    }
}