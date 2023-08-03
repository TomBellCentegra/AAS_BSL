namespace AAS_BSL.Services.Transaction.Order;

public interface IOrderRepository
{
    Task<int> Add(Domain.Entyties.Transaction.Order.Order order);
    Task Delete(string transactionId);
}