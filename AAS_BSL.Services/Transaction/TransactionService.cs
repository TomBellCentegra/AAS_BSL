using System.Data;
using AAS_BSL.Domain.Dtos.Transaction;
using AAS_BSL.Domain.Entyties.Transaction;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Transaction;

public class TransactionService : ITransactionService
{
    private readonly CentegraProcessingDbContext _dbContext;

    public TransactionService(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Add(TransactionDto transaction)
    {
        var sql =
            "INSERT INTO TDM_Transaction (TDMTransactionID,BusinessDay,CloseDate,OpenDate," +
            "IsTraining,SiteInfoId,SiteInfoName,SiteInfoTimeZone,EmployeeName,Employees," +
            "EmployeeShiftId,IsDeleted,IsOpen,IsVoided,LocalCurrency,Location,LocationId," +
            "ReceiptId,TransactionType) VALUES " +
            "(@TDMTransactionID,@BusinessDay,@CloseDate,@OpenDate,@IsTraining,@SiteInfoId," +
            "@SiteInfoName,@SiteInfoTimeZone,@EmployeeName,@Employees," +
            "@EmployeeShiftId,@IsDeleted,@IsOpen,@IsVoided,@LocalCurrency," +
            "@Location,@LocationId,@ReceiptId,@TransactionType)";

        var parameters = new DynamicParameters();
        parameters.Add("TDMTransactionID", transaction.TransactionID, DbType.String, ParameterDirection.Input);
        parameters.Add("BusinessDay", transaction.BusinessDay, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("CloseDate", transaction.CloseDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("OpenDate", transaction.OpenDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("IsTraining", transaction.IsTraining, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("SiteInfoId", transaction.SiteInfoId, DbType.String, ParameterDirection.Input);
        parameters.Add("SiteInfoName", transaction.SiteInfoName, DbType.String, ParameterDirection.Input);
        parameters.Add("SiteInfoTimeZone", transaction.SiteInfoTimeZone, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("EmployeeName", transaction.EmployeeName, DbType.String, ParameterDirection.Input);
        parameters.Add("Employees", transaction.Employees, DbType.String, ParameterDirection.Input);
        parameters.Add("EmployeeShiftId", transaction.EmployeeShiftId, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDeleted", transaction.IsDeleted, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("IsOpen", transaction.IsOpen, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("IsVoided", transaction.IsVoided, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("LocalCurrency", transaction.LocalCurrency, DbType.String, ParameterDirection.Input);
        parameters.Add("Location", transaction.Location, DbType.String, ParameterDirection.Input);
        parameters.Add("LocationId", transaction.LocationId, DbType.String, ParameterDirection.Input);
        parameters.Add("ReceiptId", transaction.ReceiptId, DbType.String, ParameterDirection.Input);
        parameters.Add("TransactionType", transaction.TransactionType, DbType.String, ParameterDirection.Input);

        using var connection = _dbContext.CreateConnection();
        var resultId = await connection.ExecuteAsync(sql,
            parameters);

        if (resultId == 0)
        {
            throw new Exception("Transaction didnt saved");
        }

        return transaction.TransactionID;
    }

    public async Task SetBatched(string transactionId, int batched)
    {
        var query =
            "UPDATE TDM_Transaction SET Batched = @batched " +
            "WHERE TDMTransactionID = @transactionId";

        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId, batched });
    }

    public async Task<Transactions> Get(string externalId)
    {
        var query = "SELECT * FROM [TDM_Transaction] TDT " +
                    "INNER JOIN TDM_Item TDI ON TDT.TDMTransactionID = TDI.TDMTransactionID WHERE TDT.TDMTransactionID = @externalId";
        using var connection = _dbContext.CreateConnection();

        var transactionMap = new Dictionary<string, Transactions>();

        await connection.QueryAsync<Transactions, Domain.Entyties.Item.Item, Transactions>(query,
            (transaction, item) =>
            {
                if (transactionMap.TryGetValue(transaction.TDMTransactionID, out Transactions existingTransactions))
                {
                    transaction = existingTransactions;
                }
                else
                {
                    transaction.Items = new List<Domain.Entyties.Item.Item>();
                    transactionMap.Add(transaction.TDMTransactionID, transaction);
                }

                transaction.Items.Add(item);
                return transaction;
            }, splitOn: "ItemID", param: new { externalId });
        return transactionMap.Values.First();
    }
}