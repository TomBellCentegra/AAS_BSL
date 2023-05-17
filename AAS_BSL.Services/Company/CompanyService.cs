using System.Data;
using AAS_BSL.Domain.Dtos;
using AAS_BSL.Infrastructure.Database;
using AAS_BSL.Services.Secret;

using Dapper;

namespace AAS_BSL.Services.Company;

public class CompanyService : ICompanyService
{
    private readonly CentegraProcessingDbContext _dbContext;
    private readonly ISecretService _secretService;

    public CompanyService(CentegraProcessingDbContext dbContext, ISecretService secretService)
    {
        _dbContext = dbContext;
        _secretService = secretService;
    }
    public async Task<Domain.Company.Company> CreateOrGet(SubscriptionRequest request)
    {
        var getQuery = "SELECT * FROM [BSL_Company] WHERE Name = @Name AND OrganizationId = @OrganizationId";
        using var connection = _dbContext.CreateConnection();
        var company = await connection.QuerySingleOrDefaultAsync<Domain.Company.Company>(getQuery,
                    new { Name = request.CompanyName, OrganizationId=request.NepOrganization });
        
        if (company is null)
        {
            var secretId = await _secretService.AddOrGet(new SecretUpdateRequestDto
                { SecretKey = request.SecretKey, SharedKey = request.SharedKey });

            var query = "INSERT INTO [BSL_Company] " +
                        "(Name, OrganizationId, CreatedDate, SecretId)" +
                        " VALUES (@Name, @OrganizationId, @CreatedDate, @SecretId)" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", request.CompanyName, DbType.String);
            parameters.Add("OrganizationId", request.NepOrganization, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("SecretId", secretId, DbType.Int32);
            
            var id = await connection.QuerySingleAsync<int>(query, parameters);

            return await Get(id);
        }


        return company;
    }

    public async Task SetSubscribed(int companyId)
    {
        var query = "UPDATE BSL_Company SET IsSubscribed = true WHERE CompanyId = @companyId";

        using var connection = _dbContext.CreateConnection();
        
        await connection.ExecuteAsync(query, new {companyId});
    }

    public Task<StatusResult> Update()
    {
        throw new NotImplementedException();
    }

    public async Task<Domain.Company.Company> Get(int id)
    {
        var query = "SELECT * FROM [BSL_Company] BC " +
                    "INNER JOIN [BSL_Secret] BS ON BC.SecretId = BS.SecretId " +
                    "WHERE BC.CompanyId = @id";
        using var connection = _dbContext.CreateConnection();
        
        var companies = await connection.QueryAsync<Domain.Company.Company, Domain.Secret.Secret, Domain.Company.Company>(query,
            (company, sectet) =>
            {
                company.Secret = sectet;
                return company;
            }, splitOn: "SecretId", param: new { id });
        return companies.FirstOrDefault();
    }

    public Task<StatusResult> UpdateSecret(SecretUpdateRequestDto request)
    {
        throw new NotImplementedException();
    }
}