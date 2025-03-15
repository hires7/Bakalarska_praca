using System.Data.SQLite;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca.Core.Services;

public static class MaterialService
{
    public static List<Material> GetAllMaterials()
    {
        var materials = new List<Material>();

        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "SELECT Id, Name, HumidityType, Coefficient FROM Materials;";
        using var command = new SQLiteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            materials.Add(new Material
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                HumidityType = reader.GetDouble(2),
                Coefficient = reader.GetDouble(3)
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

    public static bool DeleteMaterial(int id)
    {
        using var connection = DatabaseHelper.GetConnection();
        connection.Open();

        string sql = "DELETE FROM Materials WHERE Id = @id;";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() > 0;
    }
}
