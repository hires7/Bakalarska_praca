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
        public string LicensePlate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Tara { get; set; }
        public bool IsInHouse { get; set; }
    }
}
