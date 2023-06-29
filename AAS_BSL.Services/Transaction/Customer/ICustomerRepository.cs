namespace AAS_BSL.Services.Transaction.Customer;

public interface ICustomerRepository
{
    Task<int> Add(Domain.Entyties.Transaction.Customer.Customer customer);
    Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Customer.Customer> customers);
    Task Delete(string transactionId);
    
}