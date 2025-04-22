namespace ServiceManagement.Models
{
    public class IntentionServiceDbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceTypeId { get; set; }

        public string Description { get; set; }

        public decimal Limit_Area { get; set; }

        public decimal Daily_Price { get; set; }

        public bool UrbanConfrontation { get; set; }

        public bool RuralConfrontation { get; set; }

        public bool IsActive { get; set; } = true;

        // Propriedade de navegação para o tipo de serviço relacionado
        public virtual ServiceTypeDbo ServiceType { get; set; }
    }
}