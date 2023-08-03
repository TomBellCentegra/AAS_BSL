namespace AAS_BSL.Services.TransactionPayload;

public interface ITransactionPayloadService
{
    Task<int> Add(Domain.Entyties.Transaction.TransactionPayload transactionPayload);
}