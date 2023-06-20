using System.Data;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Item;

public class ItemRepository : IItemRepository
{
    private readonly CentegraProcessingDbContext _dbContext;

    public ItemRepository(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(Domain.Entyties.Item.Item item)
    {
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QuerySingleAsync<int>("INSERT INTO TDM_Item VALUES (@Discount, @UnitPrice, " +
                                                        "@Quantity, @Measurement, @ProductId, @ProductName, " +
                                                        "@ProductPrice, @ParentItemId, @TDMTransactionID) " +
                                                        "SELECT CAST(SCOPE_IDENTITY() as int)",
            item);
        return id;
    }

    public async Task BatchAdd(IEnumerable<Domain.Entyties.Item.Item> items)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("INSERT INTO TDM_Item VALUES (@Discount, @UnitPrice, " +
                                      "@Quantity, @Measurement, @ProductId, @ProductName, " +
                                      "@ProductPrice, @ParentItemId, @TDMTransactionID)",
            items);
    }

    public async Task BatchDelete(IEnumerable<Domain.Entyties.Item.Item> items)
    {
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Movies WHERE Id IN @ids",
            param: new { ids = items.Select(m => m.ItemID) });
    }

    public async Task BatchUpdate(IEnumerable<Domain.Entyties.Item.Item> items,
        IEnumerable<Domain.Entyties.Item.Item> currentItems)
    {
        var setCurrentItems = currentItems.ToDictionary(x => x.ProductId, x => x);

        foreach (var item in items)
        {
            setCurrentItems.TryGetValue(item.ProductId, out var curItem);

            await Update(curItem, item.ItemID);
        }
    }

    private async Task Update(Domain.Entyties.Item.Item item, int itemId)
    {
        var query = "UPDATE TDM_Item SET Discount = @Discount, UnitPrice = @UnitPrice, Quantity = @Quantity," +
                    "Measurement = @Measurement, ProductId = @ProductId, ProductPrice = @ProductPrice, " +
                    "ProductName = @ProductName, ParentItemId = @ParentItemId, TDMTransactionID = @TDMTransactionID WHERE ItemID = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", itemId, DbType.Int32);
        parameters.Add("Discount", item.Discount, DbType.Decimal);
        parameters.Add("UnitPrice", item.UnitPrice, DbType.Decimal);
        parameters.Add("Quantity", item.Quantity, DbType.Int32);
        parameters.Add("Measurement", item.Measurement, DbType.String);
        parameters.Add("ProductId", item.ProductId, DbType.String);
        parameters.Add("ProductPrice", item.ProductPrice, DbType.Decimal);
        parameters.Add("ProductName", item.ProductName, DbType.String);
        parameters.Add("ParentItemId", item.ParentItemId, DbType.Int32);
        parameters.Add("TDMTransactionID", item.TDMTransactionID, DbType.String);


        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }
}