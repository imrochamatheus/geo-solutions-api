using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services
{
    public interface IGeoLocationService
    {

        Task<decimal> GetDistanceFromStartEndPoint(StartPointDbo origin, DestinyDto destiny);
    }
}
