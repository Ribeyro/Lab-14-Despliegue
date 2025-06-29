using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using MediatR;

namespace Lab.Application.UseCases.Users.Commands;
public record CreateUserCommand(
    string Username,
    string Password,
    string Email
) : IRequest<string>;

internal sealed class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateUserCommand, string>
{
    public async Task<string> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await userRepository.ExistsByEmailAsync(request.Email))
            throw new Exception("El correo ya está en uso.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync();

        return "Usuario registrado con éxito.";
    }
}