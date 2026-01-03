using eSaludApi.Domain.Common.Entities;
using eSaludApi.Application.Common.Interfaces;
using eSaludApi.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSaludApi.Application.Features.Users.Commands.CreateUser
{
   public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar si el correo ya existe
            var existingUser = await _userRepository.GetByEmailAsync(request.Correo);
            if (existingUser != null)
            {
                throw new Exception("El correo ya está registrado");
            }

            // 2. Hasheamos la contraseña
            var passwordHashed = _passwordHasher.Hash(request.Contrasena);

            // 2. Crear la entidad
            var user = new User(
                request.Nombre,
                request.Apellidos,
                request.Correo,
                passwordHashed,
                request.IdRol
            );

            // 3. Guardamos en la base de datos
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
    }
}
