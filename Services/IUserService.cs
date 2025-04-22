using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;

namespace GeoSolucoesAPI.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(UserDTO dto);
        Task<User?> GetById(int id);
        Task<List<User>> GetAll();
        Task<User?> Authenticate(LoginDTO dto);
        Task<bool> UpdateUser(int id, UserDTO dto);
        Task<bool> DeleteUser(int id);
    }
}
