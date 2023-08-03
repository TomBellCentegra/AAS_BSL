using AAS_BSL.Infrastructure.Mapper;
using AAS_BSL.Services.Item.Tax;
using AAS_BSL.Services.Transaction.Discount;

namespace AAS_BSL.Services.Item;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ITaxRepository _taxRepository;
    private readonly IDiscountRepository _discountRepository;

    public ItemService(IItemRepository itemRepository, ITaxRepository taxRepository,
        IDiscountRepository discountRepository)
    {
        _itemRepository = itemRepository;
        _taxRepository = taxRepository;
        _discountRepository = discountRepository;
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
            if (item.Discounts.Any())
            {
                var discounts = item.Discounts.Select(x =>
                {
                    x.TDMItemID = itemId;
                    return x;
                });
                await _discountRepository.BatchAdd(discounts);
            }

            await _taxRepository.BatchAdd(taxes);
        }
    }
}