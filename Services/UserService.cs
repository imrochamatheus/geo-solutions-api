using GeoSolucoesAPI.DTOs;
using GeoSolucoesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GeoSolucoesAPI.Services
{
    public class UserService : IUserService
    {
        private readonly GeoSolutionsDbContext _context;

        public UserService(GeoSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(UserDTO dto)
        {
            if (!IsValidCell(dto.Cell))
                throw new ArgumentException("Número de celular inválido.");

            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists)
                throw new ArgumentException("Já existe um usuário com esse e-mail.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Cell = dto.Cell,
                UserType = dto.UserType
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> Authenticate(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;

            return user;
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Email) && user.Email != dto.Email)
            {
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                    throw new ArgumentException("O email informado já está em uso.");
                user.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Cell))
            {
                if (!IsValidCell(dto.Cell))
                    throw new ArgumentException("Número de celular inválido.");
                user.Cell = dto.Cell;
            }

            if (dto.UserType != 0)
                user.UserType = dto.UserType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePassword(int userId, ChangePasswordDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
                throw new ArgumentException("Senha atual incorreta.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool IsValidCell(string cell)
        {
            return Regex.IsMatch(cell, @"^\(?\d{2}\)?\s?9\d{4}-?\d{4}$");
        }
    }
}
