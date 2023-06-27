using System.Data;
using AAS_BSL.Domain.Entyties.Payment;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Payment;

public class TotalsRepository : ITotalsRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public TotalsRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Totals totals)
    {
        var sql =
            "INSERT INTO TDM_Totals (GrandAmount,NetAmount,TDMTransactionID,GrossAmount," +
            "VoidsAmount,DiscountAmount,TaxExclusive) VALUES " +
            "(@GrandAmount,@NetAmount,@TDMTransactionID,@GrossAmount,@VoidsAmount,@DiscountAmount,@TaxExclusive)";

        var parameters = new DynamicParameters();
        parameters.Add("GrandAmount", totals.GrandAmount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("NetAmount", totals.NetAmount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("TDMTransactionID", totals.TDMTransactionID, DbType.String, ParameterDirection.Input);
        parameters.Add("GrossAmount", totals.GrossAmount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("VoidsAmount", totals.VoidsAmount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("DiscountAmount", totals.DiscountAmount, DbType.Decimal, ParameterDirection.Input);
        parameters.Add("TaxExclusive", totals.GrandAmount, DbType.Decimal, ParameterDirection.Input);

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
        var query = "DELETE FROM TDM_Totals WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}