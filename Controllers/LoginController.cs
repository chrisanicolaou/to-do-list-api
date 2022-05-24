using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet("{email}/{password}")]
    public User GetQuery(string email, string password)
    {
        using (var context = new postgresContext())
        {
            var user = context.Users.Single(user => user.Email == email && user.Password == password);
            return user;
        }
    }
}