using System.Collections.Generic;
using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services
{
    public static class TruckService
    {
        public static void AddTruck(Truck truck)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = @"INSERT INTO Trucks (LicensePlate, Description, Tara, IsInHouse)
                           VALUES (@plate, @desc, @tara, @inhouse);";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@plate", truck.LicensePlate);
            command.Parameters.AddWithValue("@desc", truck.Description);
            command.Parameters.AddWithValue("@tara", truck.Tara);
            command.Parameters.AddWithValue("@inhouse", truck.IsInHouse ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public static List<Truck> GetAllTrucks()
        {
            var trucks = new List<Truck>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Trucks;";
            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                trucks.Add(new Truck
                {
                    Id = reader.GetInt32(0),
                    LicensePlate = reader.GetString(1),
                    Description = reader.GetString(2),
                    Tara = reader.GetDouble(3),
                    IsInHouse = reader.GetInt32(4) == 1
                });
            }

            return trucks;
        }

        public static void DeleteTruck(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "DELETE FROM Trucks WHERE Id = @id;";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public static void UpdateTruck(Truck truck)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = @"UPDATE Trucks
                           SET LicensePlate = @plate, Description = @desc, Tara = @tara, IsInHouse = @inhouse
                           WHERE Id = @id;";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@plate", truck.LicensePlate);
            command.Parameters.AddWithValue("@desc", truck.Description);
            command.Parameters.AddWithValue("@tara", truck.Tara);
            command.Parameters.AddWithValue("@inhouse", truck.IsInHouse ? 1 : 0);
            command.Parameters.AddWithValue("@id", truck.Id);

            command.ExecuteNonQuery();
        }

        public static Truck? GetTruckById(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Trucks WHERE Id = @id;";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Truck
                {
                    Id = reader.GetInt32(0),
                    LicensePlate = reader.GetString(1),
                    Description = reader.GetString(2),
                    Tara = reader.GetDouble(3),
                    IsInHouse = reader.GetInt32(4) == 1
                };
            }

            return null;
        }


    }

}
