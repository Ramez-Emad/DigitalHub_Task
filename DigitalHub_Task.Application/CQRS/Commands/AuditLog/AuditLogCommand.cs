using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.CQRS.Commands.AuditLog;

public record AuditLogCommand(
    int UserId,
    string Action,
    string EntityName,
    int EntityId,
    DateTime CreatedAt,
    string? Metadata
) : IRequest<Unit>;