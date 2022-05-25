using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SignUpController : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult<User>> SignUpUser(User newUser)
    {
        newUser.Salt = Utils.GenerateSalt();
        newUser.Password = Utils.Encrypt(newUser.Password, newUser.Salt);
        using (var context = new postgresContext())
        {
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return CreatedAtAction("SignUpUser", newUser);
        }
    }
}