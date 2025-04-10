namespace Bakalarska_praca.Core.Models;

public class Material
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double HumidityType { get; set; }
    public double Coefficient { get; set; }
    public bool IsActive { get; set; } = true;
}

