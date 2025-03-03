using BCrypt.Net;
using System.Data.SQLite;

namespace Bakalarska_praca.Core.Services;

public class UserService
{
    private const string ConnectionString = "Data Source=weighing.db;Version=3;";
    
    public static string CurrentUser { get; private set; } = "Neprihlásený";

    public bool ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT PasswordHash FROM Users WHERE Username = @username";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        var result = command.ExecuteScalar();

        if (result == null)
            return false;

        if (BCrypt.Net.BCrypt.Verify(password, result.ToString()))
        {
            CurrentUser = username;
            return true;
        }

        return false;
    }

    public static void Logout()
    {
        CurrentUser = "Neprihlásený";
    }


}

