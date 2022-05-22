using Npgsql;

public static class Utils
{
    public static async Task<int> RunCommand(NpgsqlConnection con, string cmdText) {

        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;

        cmd.CommandText = cmdText;
        var result = await cmd.ExecuteNonQueryAsync();
        return result;
    }
}