using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet()]
    public async Task<Dictionary<string, string>> Get([FromQuery]string email, [FromQuery]string password)
    {
        await using var conn = new NpgsqlConnection(Connection.cs);
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE email = ($1) AND password = ($2);", conn)
        {
            Parameters = 
            {
                new() { Value = email },
                new() { Value = password }
            }
        };

        var reader = await cmd.ExecuteReaderAsync();

        if (!reader.HasRows) {
            var error = new Dictionary<string, string>();
            error.Add("message", "Incorrect email or password");
            return error;
        }
        
        await reader.ReadAsync();

        Dictionary<string, string> user =  new Dictionary<string, string>();

        user.Add("email", reader.GetString(reader.GetOrdinal("email")));
        user.Add("password", reader.GetString(reader.GetOrdinal("password")));
        return user;
    }
}