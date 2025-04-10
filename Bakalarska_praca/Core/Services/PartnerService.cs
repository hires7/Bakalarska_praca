using System.Collections.Generic;
using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services
{
    public static class PartnerService
    {
        public static void AddPartner(Partner partner)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = @"INSERT INTO Partners 
                           (Name, Street, City, ZipCode, ICO, DIC, IC_DPH, IsSupplier, IsCustomer)
                           VALUES (@Name, @Street, @City, @ZipCode, @ICO, @DIC, @IC_DPH, @IsSupplier, @IsCustomer);";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", partner.Name);
            command.Parameters.AddWithValue("@Street", partner.Street);
            command.Parameters.AddWithValue("@City", partner.City);
            command.Parameters.AddWithValue("@ZipCode", partner.ZipCode);
            command.Parameters.AddWithValue("@ICO", partner.ICO);
            command.Parameters.AddWithValue("@DIC", partner.DIC);
            command.Parameters.AddWithValue("@IC_DPH", partner.IC_DPH);
            command.Parameters.AddWithValue("@IsSupplier", partner.IsSupplier ? 1 : 0);
            command.Parameters.AddWithValue("@IsCustomer", partner.IsCustomer ? 1 : 0);

            command.ExecuteNonQuery();
        }

        public static List<Partner> GetAllPartners()
        {
            var partners = new List<Partner>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Partners";
            using var command = new SQLiteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                partners.Add(new Partner
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Street = reader.GetString(2),
                    City = reader.GetString(3),
                    ZipCode = reader.GetString(4),
                    ICO = reader.GetString(5),
                    DIC = reader.GetString(6),
                    IC_DPH = reader.GetString(7),
                    IsSupplier = reader.GetInt32(8) == 1,
                    IsCustomer = reader.GetInt32(9) == 1
                });
            }

            return partners;
        }

        public static void DeletePartner(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "DELETE FROM Partners WHERE Id = @Id";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public static void UpdatePartner(Partner partner)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = @"UPDATE Partners 
                           SET Name = @Name, Street = @Street, City = @City, ZipCode = @ZipCode,
                               ICO = @ICO, DIC = @DIC, IC_DPH = @IC_DPH,
                               IsSupplier = @IsSupplier, IsCustomer = @IsCustomer
                           WHERE Id = @Id";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", partner.Name);
            command.Parameters.AddWithValue("@Street", partner.Street);
            command.Parameters.AddWithValue("@City", partner.City);
            command.Parameters.AddWithValue("@ZipCode", partner.ZipCode);
            command.Parameters.AddWithValue("@ICO", partner.ICO);
            command.Parameters.AddWithValue("@DIC", partner.DIC);
            command.Parameters.AddWithValue("@IC_DPH", partner.IC_DPH);
            command.Parameters.AddWithValue("@IsSupplier", partner.IsSupplier ? 1 : 0);
            command.Parameters.AddWithValue("@IsCustomer", partner.IsCustomer ? 1 : 0);
            command.Parameters.AddWithValue("@Id", partner.Id);

            command.ExecuteNonQuery();
        }

        public static Partner? GetPartnerById(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Partners WHERE Id = @id;";
            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Partner
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Street = reader.GetString(2),
                    City = reader.GetString(3),
                    ZipCode = reader.GetString(4),
                    ICO = reader.GetString(5),
                    DIC = reader.GetString(6),
                    IC_DPH = reader.GetString(7),
                    IsSupplier = reader.GetInt32(8) == 1,
                    IsCustomer = reader.GetInt32(9) == 1
                };
            }

            return null;
        }


    }
}
