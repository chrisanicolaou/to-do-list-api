using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet()]
    public User Get([FromQuery]string email, [FromQuery]string password)
    {
        using (var context = new postgresContext())
        {
            var user = context.Users.Single(user => user.Email == email && user.Password == password);
            return user;
        }
    }
}