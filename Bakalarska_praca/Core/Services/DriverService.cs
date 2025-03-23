using System.Collections.Generic;
using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services
{
    public static class DriverService
    {
        public static List<Driver> GetAllDrivers()
        {
            var drivers = new List<Driver>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Drivers;";
            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                drivers.Add(new Driver
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2)
                });
            }

            return drivers;
        }

        public static void AddDriver(Driver driver)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "INSERT INTO Drivers (FirstName, LastName) VALUES (@first, @last);";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@first", driver.FirstName);
            command.Parameters.AddWithValue("@last", driver.LastName);
            command.ExecuteNonQuery();
        }

        public static void UpdateDriver(Driver driver)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "UPDATE Drivers SET FirstName = @first, LastName = @last WHERE Id = @id;";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@first", driver.FirstName);
            command.Parameters.AddWithValue("@last", driver.LastName);
            command.Parameters.AddWithValue("@id", driver.Id);
            command.ExecuteNonQuery();
        }

        public static void DeleteDriver(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "DELETE FROM Drivers WHERE Id = @id;";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
