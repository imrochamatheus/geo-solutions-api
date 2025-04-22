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

        public async Task<bool> UpdateUser(int id, UserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            user.Email = dto.Email;
            user.Cell = dto.Cell;
            user.UserType = dto.UserType;

            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

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
