using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace eSaludApi.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand
   ( 
        string Nombre,
        string Apellidos,
        string Correo,
        string Contrasena, 
        int IdRol

   ) : IRequest<int>; // Retornamos el id del nuevo usuario
}
