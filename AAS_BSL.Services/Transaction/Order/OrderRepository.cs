using System.Data;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Transaction.Order;

public class OrderRepository : IOrderRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public OrderRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Transaction.Order.Order order)
    {
        var sql =
            "INSERT INTO TDM_Order (Channel,Number,Source,ReferenceId," +
            "ModeId,ModeName,TDMTransactionID) VALUES " +
            "(@Channel,@Number,@Source,@ReferenceId,@ModeId,@ModeName,@TDMTransactionID)";

        var parameters = new DynamicParameters();
        parameters.Add("Channel", order.Channel, DbType.String, ParameterDirection.Input);
        parameters.Add("Number", order.Number, DbType.String, ParameterDirection.Input);
        parameters.Add("Source", order.Source, DbType.String, ParameterDirection.Input);
        parameters.Add("ReferenceId", order.ReferenceId, DbType.String, ParameterDirection.Input);
        parameters.Add("ModeId", order.ModeId, DbType.String, ParameterDirection.Input);
        parameters.Add("ModeName", order.ModeName, DbType.String, ParameterDirection.Input);
        parameters.Add("TDMTransactionID", order.TDMTransactionID, DbType.String, ParameterDirection.Input);

        using var connection = _dbContext.CreateConnection();
        var resultId = await connection.ExecuteAsync(sql,
            parameters);

        if (resultId == 0)
        {
            throw new Exception("Transaction didnt saved");
        }

        return resultId;
    }

    public async Task Delete(string transactionId)
    {
        var query = "DELETE FROM TDM_Order WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}