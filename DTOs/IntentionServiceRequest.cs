namespace GeoSolucoesAPI.DTOs
{
    public class IntentionServiceRequest
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Limit_Area { get; set; }
        public decimal Daily_Price { get; set; }
    }
}
