using AAS_BSL.Domain.Logger;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Logger;

public class LoggerService : ILoggerService
{
    private readonly CentegraProcessingDbContext _dbContext;

    public LoggerService(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Save(Log log)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO TDM_Logging (TDMTransactionID, Raw, DateStamp) VALUES ('20230317-572122-10100017', @Raw, @DateStamp)",
            new { Raw = log.Raw , DateStamp = log.DateStamp});
    }

    public async Task BatchSave(List<Log> logs)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Logging VALUES (@TDMTransactionID, @Raw, @DateStamp)",
            logs);
    }
}