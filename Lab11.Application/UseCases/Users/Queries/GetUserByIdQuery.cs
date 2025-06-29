using Lab.Application.Common.Interfaces.Persistence;
using Lab.Application.DTOs;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab.Application.UseCases.Users.Queries;

public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

internal sealed class GetUserByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id);

        if (user == null)
            throw new Exception("Usuario no encontrado.");

        return new UserDto
        {
            Id = user.UserId,
            Username = user.Username
        };
    }
}