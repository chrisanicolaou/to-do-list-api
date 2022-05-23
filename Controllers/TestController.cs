using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private static readonly string[] emails = new[]
    {
        "test@test.com", "bigtesting@testing.com", "pigeonisc00l@email.com", "snuglythenugly@email.com", "ciaraclutchclutchclutch@clutch.clutch", "andy@robot.rob"
    };
    private static readonly string[] passwords = new[]
    {
        "chickenpotpie123", "dragoneatseggs224", "coocoocahoo12", "pizzaPizzaria", "spongebobsquarepants", "lesthegofar"
    };

    [HttpGet(Name = "GetTest")]
    public async Task<List<User>> Get()
    {

        User[] users = Enumerable.Range(1, 5).Select(index => new User(emails[Random.Shared.Next(emails.Length)], passwords[Random.Shared.Next(passwords.Length)]))
        .ToArray();

        await using var conn = new NpgsqlConnection(Connection.cs);
        await conn.OpenAsync();

        await Utils.RunCommand(conn, "DROP TABLE IF EXISTS users");

        await Utils.RunCommand(conn, @"CREATE TABLE users(id SERIAL PRIMARY KEY, 
        email VARCHAR(255), password VARCHAR(20))");

        foreach (var user in users)
        {
            await Utils.RunCommand(conn, $"INSERT INTO users(email, password) VALUES('{user.Email}', '{user.Password}')");
        }

        var sql = @"SELECT * FROM users";

        using var cmd = new NpgsqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();

        var result = new List<User>();

        while (reader.Read()) {
            var email = reader.GetString(reader.GetOrdinal("email"));
            var password = reader.GetString(reader.GetOrdinal("password"));
            var thisUser = new User(email, password);
            result.Add(thisUser);
        }
        await conn.CloseAsync();
        return result;
    }
}
