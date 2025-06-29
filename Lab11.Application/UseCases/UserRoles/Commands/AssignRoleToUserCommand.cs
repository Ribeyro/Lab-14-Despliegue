using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using MediatR;

namespace Lab.Application.UseCases.UserRoles.Commands;
public record AssignRoleToUserCommand(int UserId, int RoleId) : IRequest<string>;

internal sealed class AssignRoleToUserCommandHandler(IUnitOfWork _unitOfWork)
    : IRequestHandler<AssignRoleToUserCommand, string>
{
    public async Task<string> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        // Validar existencia de usuario
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("Usuario no encontrado.");

        // Validar existencia de rol
        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId);
        if (role == null)
            throw new Exception("Rol no encontrado.");

        // Verificar si ya tiene ese rol asignado
        var existingAssignment = await _unitOfWork.UserRoles.FindAsync(ur =>
            ur.UserId == request.UserId && ur.RoleId == request.RoleId);
        if (existingAssignment.Any())
            throw new Exception("El usuario ya tiene este rol asignado.");

        // Asignar nuevo rol
        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = DateTime.UtcNow
        };

        await _unitOfWork.UserRoles.AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();

        return "Rol asignado correctamente al usuario.";
    }
}