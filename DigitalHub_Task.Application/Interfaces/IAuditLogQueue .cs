using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace DigitalHub_Task.Application.Interfaces;

public interface IAuditLogQueue
{
    ValueTask EnqueueAsync(AuditLogCommand command);

    IAsyncEnumerable<AuditLogCommand> ReadAllAsync(CancellationToken ct);
}
