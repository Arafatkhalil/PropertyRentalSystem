namespace PropertyRental.Application.DTOs
{
    public class CreateLeaseDto
    {
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}