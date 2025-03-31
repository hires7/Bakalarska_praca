namespace Bakalarska_praca.Core.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ICO { get; set; } = string.Empty;
        public string DIC { get; set; } = string.Empty;
        public string IC_DPH { get; set; } = string.Empty;
        public bool IsSupplier { get; set; }
        public bool IsCustomer { get; set; }
    }
}
