using Microsoft.AspNetCore.Mvc;
namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet(Name = "GetTest")]
    public List<User> Get()
    {

        using (var context = new postgresContext())
        {
            var users = context.Users.ToList();
            return users;
        }

        // OLD CODE BELOW - to remind me of what i went through
        
        // await using var conn = new NpgsqlConnection(Connection.cs);
        // await conn.OpenAsync();

        // await Utils.RunCommand(conn, "DROP TABLE IF EXISTS users");

        // await Utils.RunCommand(conn, @"CREATE TABLE users(id SERIAL PRIMARY KEY, 
        // email VARCHAR(255), password VARCHAR(20))");

        // foreach (var user in users)
        // {
        //     await Utils.RunCommand(conn, $"INSERT INTO users(email, password) VALUES('{user.Email}', '{user.Password}')");
        // }

        // var sql = @"SELECT * FROM users";

        // using var cmd = new NpgsqlCommand(sql, conn);
        // using var reader = await cmd.ExecuteReaderAsync();

        // var result = new List<User>();

        // while (reader.Read()) {
        //     var email = reader.GetString(reader.GetOrdinal("email"));
        //     var password = reader.GetString(reader.GetOrdinal("password"));
        //     var thisUser = new CurrentUser(email, password);
        //     result.Add(thisUser);
        // }
        // await conn.CloseAsync();
    }
}
