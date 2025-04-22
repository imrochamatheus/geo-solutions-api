using ServiceManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class ServiceTypeDbo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [ForeignKey("BudgetId")]
    public int? BudgetId { get; set; }
   
    public virtual BudgetDbo Budget { get; set; }
    public bool IsActive { get; set; } = true;

    // Propriedade de navegação para a coleção de intuitos de serviço
    public virtual ICollection<IntentionServiceDbo> IntentionServices { get; set; }
}