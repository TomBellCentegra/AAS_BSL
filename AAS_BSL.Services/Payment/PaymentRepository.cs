using System.Data;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Payment;

public class PaymentRepository : IPaymentRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public PaymentRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Domain.Entyties.Payment.Payment payment)
    {
        var sql =
            "INSERT INTO TDM_Payment (ExternalPaymentID,Type,TDMTransactionID,Amount," +
            "Currency) VALUES " +
            "(@ExternalPaymentID,@Type,@TDMTransactionID,@Amount,@Currency)";

        var parameters = new DynamicParameters();
        parameters.Add("ExternalPaymentID", payment.ExternalPaymentID, DbType.Int32, ParameterDirection.Input);
        parameters.Add("Type", payment.Type, DbType.String, ParameterDirection.Input);
        parameters.Add("TDMTransactionID", payment.TDMTransactionID, DbType.String, ParameterDirection.Input);
        parameters.Add("Amount", payment.Amount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("Currency", payment.Currency, DbType.String, ParameterDirection.Input);

        using var connection = _dbContext.CreateConnection();
        var resultId = await connection.ExecuteAsync(sql,
            parameters);

        if (resultId == 0)
        {
            throw new Exception("Transaction didnt saved");
        }
    }

    public async Task Delete(string transactionId)
    {
        var query = "DELETE FROM TDM_Payment WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}