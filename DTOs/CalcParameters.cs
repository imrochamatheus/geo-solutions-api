using GeoSolucoesAPI.Helpers;

namespace GeoSolucoesAPI.DTOs
{
    public class CalcParameters
    {
        public DestinyDto Address { get; set; }
        public AreaParameters AreaSettings { get; set; }
        public int Confrontations { get; set; } = 0;
        public int ServiceTypeId { get; set; }
        public int IntentionServiceId  { get; set; }
    }
}
