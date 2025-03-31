using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services;

public static class MaterialService
{
    public static List<Material> GetAllMaterials(bool includeInactive = false)
    {
        var materials = new List<Material>();

        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = includeInactive
            ? "SELECT * FROM Materials"
            : "SELECT * FROM Materials WHERE IsActive = 1";

        using var command = new SQLiteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            materials.Add(new Material
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString() ?? "",
                HumidityType = Convert.ToDouble(reader["HumidityType"]),
                Coefficient = Convert.ToDouble(reader["Coefficient"]),
                IsActive = Convert.ToInt32(reader["IsActive"]) == 1
            });
        }

        return materials;
    }


    public static bool AddMaterial(Material material)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "INSERT INTO Materials (Name, HumidityType, Coefficient) VALUES (@name, @humidityType, @coefficient);";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@name", material.Name);
        command.Parameters.AddWithValue("@humidityType", material.HumidityType);
        command.Parameters.AddWithValue("@coefficient", material.Coefficient);

        return command.ExecuteNonQuery() > 0;
    }

    public static bool UpdateMaterial(Material material)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "UPDATE Materials SET Name = @name, HumidityType = @humidityType, Coefficient = @coefficient WHERE Id = @id;";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@name", material.Name);
        command.Parameters.AddWithValue("@humidityType", material.HumidityType);
        command.Parameters.AddWithValue("@coefficient", material.Coefficient);
        command.Parameters.AddWithValue("@id", material.Id);

        return command.ExecuteNonQuery() > 0;
    }

    public static void DeleteMaterial(int id)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "UPDATE Materials SET IsActive = 0 WHERE Id = @id";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

}
