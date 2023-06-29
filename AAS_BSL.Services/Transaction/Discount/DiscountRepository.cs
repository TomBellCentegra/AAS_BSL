using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Transaction.Discount;

public class DiscountRepository : IDiscountRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public DiscountRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Transaction.Discount.Discount discount)
    {
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QuerySingleAsync<int>("INSERT INTO TDM_Discount VALUES (@Name, @Amount, " +
                                                        "@Type, @TDMTransactionID)" +
                                                        "SELECT CAST(SCOPE_IDENTITY() as int)",
            discount);
        return id;
    }

    public async Task BatchAdd(IEnumerable<Domain.Entyties.Transaction.Discount.Discount> discounts)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Discount VALUES (@Name, @Amount, " +
                                      "@Type, @TDMTransactionID, @TDMItemID)",
            discounts);
    }
}