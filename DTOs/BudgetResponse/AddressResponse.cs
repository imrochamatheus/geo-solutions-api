namespace GeoSolucoesAPI.DTOs.BudgetResponse
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string Zipcode { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Number { get; set; }
        public string Complement { get; set; }
    }
}
