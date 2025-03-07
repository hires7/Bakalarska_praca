using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using BCrypt.Net;

namespace Bakalarska_praca.Core.Services;

public class UserService
{
    private const string ConnectionString = "Data Source=weighing.db;Version=3;";

    public static string CurrentUser { get; private set; } = "Neprihlásený";

    public bool ValidateUser(string username, string password)
    {
        User? user = GetUser(username);

        if (user == null)
            return false;

        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            CurrentUser = user.Username;
            Console.WriteLine($"Prihlásený používateľ: {CurrentUser}");
            return true;
        }

        return false;
    }


    public static void Logout()
    {
        CurrentUser = "Neprihlásený";
        Console.WriteLine("Odhlasujem používateľa...");
    }

    public User? GetUser(string username)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT Id, Username, PasswordHash, Role FROM Users WHERE Username = @username";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                Role = reader.GetString(3)
            };
        }

        return null;
    }



}
