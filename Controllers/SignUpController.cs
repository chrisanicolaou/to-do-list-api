using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SignUpController : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult<UserDTO>> SignUpUser(UserDTO newUser)
    {
        var userToAdd = new User();
        userToAdd.Email = newUser.Email;
        userToAdd.Salt = Utils.GenerateSalt();
        userToAdd.Password = Utils.Encrypt(newUser.Password, userToAdd.Salt);
        using (var context = new postgresContext())
        {
            context.Users.Add(userToAdd);
            await context.SaveChangesAsync();
            return CreatedAtAction("SignUpUser", newUser);
        }
    }
}