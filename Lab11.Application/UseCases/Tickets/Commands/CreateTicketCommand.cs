using Lab.Application.Common.Interfaces.Persistence;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab.Application.UseCases.Tickets.Commands;

public record CreateTicketCommand(
    int UserId,
    string Title,
    string Description
) : IRequest<string>;

internal sealed class CreateTicketCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateTicketCommand, string>
{
    public async Task<string> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = "Abierto",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Tickets.AddAsync(ticket);
        await _unitOfWork.SaveChangesAsync();

        return "Ticket creado exitosamente.";
    }
}