namespace GeoSolucoesAPI.Models
{
    public class ConfrontationDbo
    {
        public int Id { get; set; }
        public bool UrbanConfrontation { get; set; }
        public bool RuralConfrontation { get; set; }
        public decimal AreaMin { get; set; }
        public decimal AreaMax { get; set; }
        public decimal Price { get; set; }
    }
}
