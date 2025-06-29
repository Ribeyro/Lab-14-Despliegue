using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab.Application.UseCases.Roles.Commands;

public record CreateRoleCommand(string RoleName) : IRequest<string>;

internal sealed class CreateRoleCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateRoleCommand, string>
{
    public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _unitOfWork.Roles.FindAsync(r => r.RoleName == request.RoleName);
        if (existingRole.Any())
            throw new Exception("El rol ya existe.");

        var role = new Role
        {
            RoleName = request.RoleName
        };

        await _unitOfWork.Roles.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return "Rol creado exitosamente.";
    }
}