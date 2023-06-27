using AAS_BSL.Domain.Entyties.Payment;

namespace AAS_BSL.Services.Payment;

public interface ITotalsRepository
{
    Task Add(Totals totals);
    Task Delete(string transactionId);
}