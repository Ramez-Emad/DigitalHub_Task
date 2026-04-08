using DigitalHub_Task.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.CQRS.Commands.AuditLog;

public class AuditLogCommandHandler : IRequestHandler<AuditLogCommand, Unit>
{
    private readonly IAuditLogRepository _repo;

    public AuditLogCommandHandler(IAuditLogRepository repo)
    {
        _repo = repo;
    }

    public async Task<Unit> Handle(AuditLogCommand command, CancellationToken ct)
    {
        var log = new Domain.Entities.AuditLog
        {
            UserId = command.UserId,
            Action = command.Action,
            EntityName = command.EntityName,
            EntityId = command.EntityId,
            CreatedAt = command.CreatedAt,
            Metadata = command.Metadata
        };

        //Thread.Sleep(1500);

        _repo.AddAuditLog(log);
        await _repo.SaveChangesAsync(ct);

        return Unit.Value;
    }
}