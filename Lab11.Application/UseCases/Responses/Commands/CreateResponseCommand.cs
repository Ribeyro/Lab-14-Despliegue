using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab.Application.UseCases.Responses.Commands;

public record CreateResponseCommand(
    int TicketId,
    int ResponderId,
    string Message
) : IRequest<string>;

internal sealed class CreateResponseCommandHandler(IUnitOfWork _unitOfWork)
    : IRequestHandler<CreateResponseCommand, string>
{
    public async Task<string> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Tickets.GetByIdAsync(request.TicketId);
        if (ticket == null)
            throw new Exception("El ticket no existe.");

        var user = await _unitOfWork.Users.GetByIdAsync(request.ResponderId);
        if (user == null)
            throw new Exception("El usuario que responde no existe.");

        var response = new Response
        {
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Responses.AddAsync(response);
        await _unitOfWork.SaveChangesAsync();

        return "Respuesta creada exitosamente.";
    }
}