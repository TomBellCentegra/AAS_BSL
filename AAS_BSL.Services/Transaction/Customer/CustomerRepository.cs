using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Transaction.Customer;

public class CustomerRepository : ICustomerRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public CustomerRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Transaction.Customer.Customer customer)
    {
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QuerySingleAsync<int>("INSERT INTO TDM_Customer VALUES (@CustomerType, @Email, " +
                                                        "@Name,@BirthDate,@PhoneNumber, @TDMTransactionID)" +
                                                        "SELECT CAST(SCOPE_IDENTITY() as int)",
            customer);
        return id;
    }

    public async Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Customer.Customer> customers)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Customer VALUES (@CustomerType, @Email, " +
                                      "@Name,@BirthDate,@PhoneNumber, @TDMTransactionID)" +
                                      "SELECT CAST(SCOPE_IDENTITY() as int)",
            customers);
    }

    public async Task Delete(string transactionId)
    {
        var query = "DELETE FROM TDM_Customer WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}