using System.Data.SQLite;
using System.Windows;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;
using BCrypt.Net;

namespace Bakalarska_praca.Core.Services;

public class UserService
{
    private const string ConnectionString = "Data Source=weighing.db;Version=3;";

    public static string CurrentUser => LoggedInUser?.Username ?? "Neprihlásený";

    public static User? LoggedInUser { get; private set; } = null;

    public bool ValidateUser(string username, string password)
    {
        User? user = GetUser(username);

        if (user == null)
            return false;

        if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            LoggedInUser = user;
            Console.WriteLine($"Prihlásený používateľ: {CurrentUser}");
            return true;
        }

        return false;
    }

    public static void Logout()
    {
        Console.WriteLine($"Odhlasujem používateľa: {CurrentUser}");
        LoggedInUser = null;
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

    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT PasswordHash FROM Users WHERE Username = @username";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        var result = command.ExecuteScalar();

        if (result == null)
        {
            Console.WriteLine("Používateľ neexistuje.");
            return false;
        }

        string storedHash = result.ToString() ?? string.Empty;

        if (!BCrypt.Net.BCrypt.Verify(oldPassword, storedHash))
        {
            Console.WriteLine("Nesprávne staré heslo!");
            return false;
        }

        string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

        string updateSql = "UPDATE Users SET PasswordHash = @newPassword WHERE Username = @username";
        using var updateCommand = new SQLiteCommand(updateSql, connection);
        updateCommand.Parameters.AddWithValue("@username", username);
        updateCommand.Parameters.AddWithValue("@newPassword", newHash);
        int rowsAffected = updateCommand.ExecuteNonQuery();

        return rowsAffected > 0;
    }

    public static List<User> GetAllUsers()
    {
        List<User> users = new();

        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string sql = "SELECT Id, Username, PasswordHash, Role FROM Users";
        using var command = new SQLiteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            users.Add(new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                PasswordHash = reader.GetString(2),
                Role = reader.GetString(3)
            });
        }

        return users;
    }

    public static bool AddUser(string username, string password, bool isAdmin)
    {
        using var connection = new SQLiteConnection(ConnectionString);
        connection.Open();

        string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @username";
        using var checkCommand = new SQLiteCommand(checkSql, connection);
        checkCommand.Parameters.AddWithValue("@username", username);
        int userExists = Convert.ToInt32(checkCommand.ExecuteScalar());

        if (userExists > 0)
        {
            return false;
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        string role = isAdmin ? "Admin" : "User";

        string insertSql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@username, @password, @role)";
        using var insertCommand = new SQLiteCommand(insertSql, connection);
        insertCommand.Parameters.AddWithValue("@username", username);
        insertCommand.Parameters.AddWithValue("@password", hashedPassword);
        insertCommand.Parameters.AddWithValue("@role", role);

        int rowsAffected = insertCommand.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public static void DeleteUser(int userId)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "DELETE FROM Users WHERE Id = @id;";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", userId);
        command.ExecuteNonQuery();
    }

    public static bool UpdateUser(User user)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "UPDATE Users SET Username = @username, Role = @role WHERE Id = @id;";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@username", user.Username);
        command.Parameters.AddWithValue("@role", user.Role);
        command.Parameters.AddWithValue("@id", user.Id);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }
}
