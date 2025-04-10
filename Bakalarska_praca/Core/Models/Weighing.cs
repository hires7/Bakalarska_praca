namespace Bakalarska_praca.Core.Models;

public class Weighing
{
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public DateTime BruttoTime { get; set; }
    public DateTime TaraTime { get; set; }

    public int Truck_Id { get; set; }
    public int Partner_Id { get; set; }
    public int Material_Id { get; set; }
    public int User_Id { get; set; }

    public double Brutto { get; set; }
    public double Tara { get; set; }
    public double Netto => Brutto - Tara;

    public string Note { get; set; } = string.Empty;

    public bool IsIncoming { get; set; }
}


