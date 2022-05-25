using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpGet("{email}/{password}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetQuery(string email, string password)
    {
        using (var context = new postgresContext())
        {
            try {
                var user = context.Users.Single(user => user.Email == email);
                var checkPass = Utils.Encrypt(password, user.Salt);
                if (checkPass != user.Password) {
                    return NotFound();
                }
                return Ok(user);
            } catch {
                return NotFound();
            }
        }
    }
}

// Should the Login/SignUps have separate controllers? Or should there be one "UsersController"
// Similar to ToDoController? Unsure on best practice