using AAS_BSL.Domain.Dtos.Transaction;
using AAS_BSL.Domain.Entyties.Transaction;

namespace AAS_BSL.Services.Transaction;

public interface ITransactionService
{
    Task<string> Add(TransactionDto transaction);
    Task SetBatched(string transactionId,int batched);
    Task<Transactions> Get(string transactionId);
    Task SetRemove(string transactionId);

}