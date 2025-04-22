using ServiceManagement.DTOs;
using ServiceManagement.Models;

namespace GeoSolucoesAPI.DTOs
{
    public class ServiceTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public int? BudgetId { get; set; }
        public virtual BudgetDbo Budget { get; set; }



        // Propriedade de navegação para a coleção de intuitos de serviço
        public virtual List<IntentionServiceDto> IntentionServices { get; set; }
    }
}
