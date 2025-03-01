using System;
using System.Data.SQLite;

namespace Bakalarska_praca.Data.Database
{
    public class DatabaseInitializer
    {
        public static void Initialize()
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Users (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Username TEXT UNIQUE NOT NULL,
                                PasswordHash TEXT NOT NULL,
                                Role TEXT NOT NULL);";

            using var command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            Console.WriteLine("Tabulka USERS vytvorena");

        }

        public static void CreateAdminUser()
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string checkUser = "SELECT COUNT(*) FROM Users WHERE Username = 'admin';";
            using var checkCommand = new SQLiteCommand(checkUser, connection);
            int userExists = Convert.ToInt32(checkCommand.ExecuteScalar());

            if (userExists == 0)
            {
                string hash = BCrypt.Net.BCrypt.HashPassword("admin123"); // docasne hashovanie, TODO pozriet nieco dalsie
                string sql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES ('admin', @password, 'Admin');";

                using var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@password", hash);
                command.ExecuteNonQuery();

                Console.WriteLine("Admin ucet vytvoreny");
            }
        }



    }
}
