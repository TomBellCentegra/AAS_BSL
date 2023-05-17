using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AAS_BSL.Infrastructure.Database;

public class CentegraProcessingDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public CentegraProcessingDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_Centegra_CloudProcessing_ConnString");

        if (string.IsNullOrEmpty(_connectionString))
        {
            _connectionString = _configuration.GetConnectionString("CentegraCloudProcessing");
        }
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}