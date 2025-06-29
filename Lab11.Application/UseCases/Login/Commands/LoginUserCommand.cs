using Lab11.Domain.Interfaces.IServices;
using MediatR;

namespace Lab.Application.UseCases.Users.Commands;

public record LoginUserCommand(
    string UserName,
    string Password) : IRequest<string>;

internal sealed class LoginUserCommandHandler(
    ILoginUseCase loginUseCase
) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string>
        Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await loginUseCase.LoginAsync(request.UserName, request.Password);
    }
}