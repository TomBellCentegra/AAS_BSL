using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Transaction.Employee;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public EmployeeRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Transaction.Emploee.Employee employee)
    {
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QuerySingleAsync<int>("INSERT INTO TDM_Employee VALUES (@Name, @RoleName, " +
                                                        "@RoleId,@ShiftId, @TDMTransactionID)" +
                                                        "SELECT CAST(SCOPE_IDENTITY() as int)",
            employee);
        return id;
    }

    public async Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Emploee.Employee> employees)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Employee VALUES (@Name, @RoleName, " +
                                      "@RoleId,@ShiftId, @TDMTransactionID)" +
                                      "SELECT CAST(SCOPE_IDENTITY() as int)",
            employees);
    }

    public async Task Delete(string transactionId)
    {
        var query = "DELETE FROM TDM_Employee WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}