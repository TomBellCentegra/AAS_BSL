using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.TransactionPayload;

public class TransactionPayloadService : ITransactionPayloadService
{
    private readonly CentegraProcessingDbContext _dbContext;

    public TransactionPayloadService(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Transaction.TransactionPayload transactionPayload)
    {
        var sql =
            "INSERT INTO [TDM_Transaction_Payload] VALUES (@TDMTransactionID,@Payload)";

        using var connection = _dbContext.CreateConnection();
        var resultId = await connection.ExecuteAsync(sql,
            new { TDMTransactionID = transactionPayload.TDMTransactionsID, Payload = transactionPayload.Payload });

        return resultId;
    }
}