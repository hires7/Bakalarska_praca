using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalarska_praca.Core.Models
{
    public class Truck
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty; // ŠPZ
        public string Description { get; set; } = string.Empty;
        public double Tara { get; set; } // hmotnosť v kg
        public bool IsInHouse { get; set; }
    }
}
