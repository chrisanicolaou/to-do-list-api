namespace dotnet_backend;

public class CurrentUser
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    public CurrentUser(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
