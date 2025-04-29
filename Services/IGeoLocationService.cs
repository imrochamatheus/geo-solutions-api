using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.DTOs.BudgetResponse;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services
{
    public interface IGeoLocationService
    {

        Task<decimal> GetDistanceFromStartEndPoint(StartPointDbo origin, DestinyDto destiny);
        Task<AddressResponse> GetAddressByCep(string cep);
    }
}
