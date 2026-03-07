namespace PropertyRental.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }  
        public string Name { get; set; } = string.Empty;  
        public string Address { get; set; } = string.Empty;  
        public string City { get; set; } = string.Empty;  
        public decimal MonthlyPrice { get; set; } 
        public bool IsAvailable { get; set; } = true; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

        // Navigation Property: عقار واحد يمكن أن يكون له عدة عقود إيجار عبر الزمن
        public ICollection<Lease> Leases { get; set; } = new List<Lease>();
    }
}