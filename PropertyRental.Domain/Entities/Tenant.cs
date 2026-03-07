namespace PropertyRental.Domain.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty; 

        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}