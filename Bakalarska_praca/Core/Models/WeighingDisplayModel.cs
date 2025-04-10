public class WeighingDisplayModel
{
    public int Id { get; set; }
    public string Type => IsIncoming ? "Príjemka" : "Výdajka";
    public DateTime DateTime => BruttoTime;
    public string Partner { get; set; } = "";
    public string Truck { get; set; } = "";
    public string User { get; set; } = "";
    public string Material { get; set; } = "";
    public double Brutto { get; set; }
    public double Tara { get; set; }
    public double Netto => Brutto - Tara;
    public string Note { get; set; } = "";
    public bool IsIncoming { get; set; }
    public DateTime BruttoTime { get; set; }
}
