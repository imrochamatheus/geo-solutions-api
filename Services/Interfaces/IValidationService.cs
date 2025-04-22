using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.Request;
using GeoSolucoesAPI.Services.Validations;
using ServiceManagement.DTOs;

namespace GeoSolucoesAPI.Services.Interfaces
{
    public interface IValidationService
    {
       void CalcValidation(CalcParameters calcParameters);
       void BudgetValidation(BudgetRequest budgetRequest);
        void ValidateServiceType(ServiceTypeRequest serviceTypeRequest);
        void ValidateIntentionService(IntentionServiceDto intentionServiceDto);
    }
}
