﻿namespace GeoSolucoesAPI.Services.Validations
{
    public class ValidationError
    {

        public string Field { get; set; }
        public string Message { get; set; }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }

    }
}
