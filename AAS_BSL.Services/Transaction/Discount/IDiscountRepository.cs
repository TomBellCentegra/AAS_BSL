namespace AAS_BSL.Services.Transaction.Discount;

public interface IDiscountRepository
{
    Task<int> Add(Domain.Entyties.Transaction.Discount.Discount discount);
    Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Discount.Discount> discounts);
}