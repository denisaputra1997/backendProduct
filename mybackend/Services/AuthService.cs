using System.Data;
using mybackend.Models;
using mybackend.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace mybackend.Services;

public interface IAuthService
{
    string Authenticate(string username, string password);
    User GetUserByUsername(string username);
}

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly MySqlConnection _connection;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }

    public string Authenticate(string username, string password)
    {
        using (IDbConnection connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {

            var user = connection.QueryFirstOrDefault<User>("SELECT * FROM Operator WHERE username = @Username AND password = @Password", new { Username = username, Password = password });
            if (username == user?.Username && password == user?.Password)
            {
                return TokenGenerator.GenerateToken(_configuration, user);
            }
            return null;

        }
    }

    public User GetUserByUsername(string username)
    {
        _connection.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM Operator WHERE Username = @Username", _connection);
        cmd.Parameters.AddWithValue("@Username", username);
        var reader = cmd.ExecuteReader();

        User user = null;
        if (reader.Read())
        {
            user = new User
            {
                OperatorId = reader.GetInt32("OperatorID"),
                Nama = reader.GetString("Nama"),
                Username = reader.GetString("Username"),
                Password = reader.GetString("Password"),
                TanggalBergabung = reader.GetDateTime("TanggalBergabung"),
                CreatedAt = reader.GetDateTime("CreatedAt"),
                UpdatedAt = reader.GetDateTime("UpdatedAt"),
                CreatedBy = reader.GetInt32("CreatedBy"),
                ModifiedBy = reader.GetInt32("ModifiedBy"),
                Status = reader.GetString("Status")
            };
        }

        _connection.Close();
        return user;
    }
}