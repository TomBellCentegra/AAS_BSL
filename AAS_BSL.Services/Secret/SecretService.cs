using AAS_BSL.Domain.Dtos;
using AAS_BSL.Infrastructure.Database;
using Dapper;

namespace AAS_BSL.Services.Secret;

public class SecretService : ISecretService
{
    private readonly CentegraProcessingDbContext _dbContext;

    public SecretService(CentegraProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddOrGet(SecretUpdateRequestDto request)
    {
        var query = "SELECT * FROM [BSL_Secret] WHERE SharedKey = @SharedKey AND SecretKey = @SecretKey";

        using var connection = _dbContext.CreateConnection();
        var secret =
            await connection.QuerySingleOrDefaultAsync<Domain.Secret.Secret>(query,
                new { request.SharedKey, request.SecretKey });
        int resultId;
        if (secret == null)
        {
            var sql = "INSERT INTO [BSL_Secret] VALUES (@SharedKey,@SecretKey) SELECT CAST(SCOPE_IDENTITY() as int)";
            resultId = await connection.QuerySingleAsync<int>(sql, new { request.SharedKey, request.SecretKey });
        }
        else
        {
            resultId = secret.SecretId;
        }

        return resultId;
    }
    
}