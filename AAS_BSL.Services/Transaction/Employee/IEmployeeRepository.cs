namespace AAS_BSL.Services.Transaction.Employee;

public interface IEmployeeRepository
{
    Task<int> Add(Domain.Entyties.Transaction.Emploee.Employee employee);
    Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Emploee.Employee> employees);
    Task Delete(string transactionId);
}