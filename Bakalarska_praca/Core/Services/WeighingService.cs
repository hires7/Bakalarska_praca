using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services
{
    public static class WeighingService
    {
        public static void AddWeighing(Weighing weighing)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            var sql = @"INSERT INTO Weighings 
                (Date, BruttoTime, TaraTime, IsIncoming, Truck_Id, Partner_Id, Material_Id, User_Id, Brutto, Tara, Note)
                VALUES (@Date, @BruttoTime, @TaraTime, @IsIncoming, @Truck_Id, @Partner_Id, @Material_Id, @User_Id, @Brutto, @Tara, @Note);";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@Date", weighing.Date.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@BruttoTime", weighing.BruttoTime.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@TaraTime", weighing.TaraTime.ToString("HH:mm:ss"));
            command.Parameters.AddWithValue("@IsIncoming", weighing.IsIncoming ? 1 : 0);
            command.Parameters.AddWithValue("@Truck_Id", weighing.Truck_Id);
            command.Parameters.AddWithValue("@Partner_Id", weighing.Partner_Id);
            command.Parameters.AddWithValue("@Material_Id", weighing.Material_Id);
            command.Parameters.AddWithValue("@User_Id", weighing.User_Id);
            command.Parameters.AddWithValue("@Brutto", weighing.Brutto);
            command.Parameters.AddWithValue("@Tara", weighing.Tara);
            command.Parameters.AddWithValue("@Note", weighing.Note);

            command.ExecuteNonQuery();
        }

        public static List<Weighing> GetAllWeighings()
        {
            var result = new List<Weighing>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            var sql = "SELECT * FROM Weighings ORDER BY Date DESC, BruttoTime DESC";

            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var weighing = new Weighing
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Date = DateTime.Parse(reader["Date"].ToString()!),
                    BruttoTime = DateTime.ParseExact(reader["BruttoTime"].ToString()!, "HH:mm:ss", null),
                    TaraTime = DateTime.ParseExact(reader["TaraTime"].ToString()!, "HH:mm:ss", null),
                    IsIncoming = Convert.ToInt32(reader["IsIncoming"]) == 1,
                    Truck_Id = Convert.ToInt32(reader["Truck_Id"]),
                    Partner_Id = Convert.ToInt32(reader["Partner_Id"]),
                    Material_Id = Convert.ToInt32(reader["Material_Id"]),
                    User_Id = Convert.ToInt32(reader["User_Id"]),
                    Brutto = Convert.ToDouble(reader["Brutto"]),
                    Tara = Convert.ToDouble(reader["Tara"]),
                    Note = reader["Note"].ToString() ?? ""
                };

                result.Add(weighing);
            }

            return result;
        }
    }
}
