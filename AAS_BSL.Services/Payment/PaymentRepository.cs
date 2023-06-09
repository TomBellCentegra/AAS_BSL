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
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO TDM_Payment VALUES (@ExternalPaymentID, @Type,@TDMTransactionID, @Amount, @Currency, @Change, " +
            "@GrandAmount, @NetAmount, @GrossAmount, @VoidsAmount, @DiscountAmount, @TaxExclusive)",
            payment);
    }

    public async Task Delete(string transactionId)
    {
        var query = "DELETE FROM TDM_Payment WHERE TDMTransactionID = @transactionId";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new { transactionId });
    }
}