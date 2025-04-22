namespace GeoSolucoesAPI.Services.Validations
{
    public class ValidationException : Exception
    {
        public bool IsValid => Errors.Count == 0;
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public ValidationException(string message, List<ValidationError> errors) : base(message) 
        { 
            Errors = errors;
        }
    }
}
