namespace PropertyRental.Application.DTOs
{
    public class CreatePropertyDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal MonthlyPrice { get; set; }
    }
}
