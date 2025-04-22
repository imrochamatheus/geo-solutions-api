namespace ServiceManagement.DTOs
{
    public class IntentionServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceTypeId { get; set; } 
        public string ServiceTypeName { get; set; }
        public decimal Limit_Area { get; set; }
        public decimal Daily_Price { get; set; }

        public bool UrbanConfrontation { get; set; }
        public bool RuralConfrontation { get; set; }

    }
}