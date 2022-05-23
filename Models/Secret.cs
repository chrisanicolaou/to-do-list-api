namespace dotnet_backend;

public class Connection
{
    public string? username { get; set; }
    public string? password { get; set; }
    public string? engine { get; set; }
    public string? host { get; set; }
    public int? port { get; set; }
    public string? dbCluster { get; set; }


    // public Secret(string username, string password, string Engine, string Host, int Port, string DbCluster)
    // {
    //     Username = username;
    //     Password = password;
    // }
}
