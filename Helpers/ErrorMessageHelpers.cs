namespace GeoSolucoesAPI.Helpers
{
    public static class ErrorMessageHelpers
    {
        public static string ServiceNotFound => $"Tipo de serviço não encontrado, {FunctionHelpers.ContactAdm()}";
        public static string IntentionServiceNotFound 
            => $"A intenção de serviço não encontrada ou não faz parte do tipo de serviço, {FunctionHelpers.ContactAdm()}";
        public static string BudgetNotFound => $"Orçamento não encontrado na base de dados";

        public static string BudgetUpdateError => "Erro durante atualização de orçamento de orçamento";

        public static string GetBudgetError => "Forneça um ID válido para orçamento";

    }
}
