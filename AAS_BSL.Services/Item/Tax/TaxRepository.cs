using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Item.Tax;

public class TaxRepository : ITaxRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public TaxRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BatchAdd(IEnumerable<Domain.Entyties.Item.Tax.Tax> taxes)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Item_Taxes VALUES (@Name, @ExternalId, " +
                                      "@Type, @TaxableAmount, @Amount, @ItemId)",
            taxes);
    }
}