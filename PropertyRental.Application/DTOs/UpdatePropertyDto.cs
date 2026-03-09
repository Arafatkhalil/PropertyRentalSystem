namespace PropertyRental.Application.DTOs
{
    public class UpdatePropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal MonthlyPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}
