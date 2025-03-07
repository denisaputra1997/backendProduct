using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace mybackend.Data;

public class MySqlConnectionProvider
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public MySqlConnectionProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

}
