using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoSolucoesAPI.Models
{
    public class CityDbo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
