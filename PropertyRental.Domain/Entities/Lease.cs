namespace PropertyRental.Domain.Entities
{
    public class Lease
    {
        public int Id { get; set; }
        public int PropertyId { get; set; } // Foreign Key
        public int TenantId { get; set; }   // Foreign Key
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyPrice { get; set; }

        // (Navigation Properties)
        public Property Property { get; set; } = null!;
        public Tenant Tenant { get; set; } = null!;
    }
}