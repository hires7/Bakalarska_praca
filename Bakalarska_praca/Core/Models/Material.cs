namespace Bakalarska_praca.Core.Models;

public class Material
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required double HumidityType { get; set; }
    public required double Coefficient { get; set; }
}
