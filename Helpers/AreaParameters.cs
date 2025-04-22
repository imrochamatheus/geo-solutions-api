namespace GeoSolucoesAPI.Helpers
{
    public class AreaParameters
    {
        public decimal Area_Size { get; set; }
        public EUnitOfMeasure UnitOfMeasure { get; set; }
    }

    public enum EUnitOfMeasure
    {
        SquareMeter,
        Hectare
    }
}
