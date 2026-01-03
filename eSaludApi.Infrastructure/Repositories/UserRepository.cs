using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eSaludApi.Domain.Common.Entities;
using eSaludApi.Domain.Interfaces;
using eSaludApi.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace eSaludApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly eSaludDbContext _context;

        public UserRepository(eSaludDbContext context) => _context = context;

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

        public async Task AddAsync(User user) => await _context.Usuarios.AddAsync(user);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}

