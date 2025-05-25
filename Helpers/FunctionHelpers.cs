namespace GeoSolucoesAPI.Helpers
{
    public static class FunctionHelpers
    {
        public static bool IsNotNullAndAny<T>(this List<T> list) => list != null && list.Count > 0;
        public static decimal ConvertHectarToSquareMeter(this decimal hectareSize) => hectareSize * 10000;
        public static decimal ConvertSquareMeterToHectare(this decimal SquareMeterSize) => SquareMeterSize / 10000;
        public static decimal ConvertToQuilometer(this decimal Meter) => Meter / 1000;
        public static decimal ConvertToMeter(this decimal Quilometer) => Quilometer * 1000;

        public static float ConvertToMeterFloat(this float Quilometer) => Quilometer * 1000;
        public static string ContactAdm() => "Por favor, entre em contato com o administrador do sistema";
    }
}
