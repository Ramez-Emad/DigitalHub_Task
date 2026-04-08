using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using DigitalHub_Task.Application.Interfaces;
using System.Threading.Channels;

namespace DigitalHub_Task.Infrastructure.BackgroundServices;

public class AuditLogQueue : IAuditLogQueue
{
    private readonly Channel<AuditLogCommand> _channel;

    public AuditLogQueue()
    {
        _channel = Channel.CreateUnbounded<AuditLogCommand>();
    }

    public async ValueTask EnqueueAsync(AuditLogCommand command)
    {
        await _channel.Writer.WriteAsync(command);
    }

    public IAsyncEnumerable<AuditLogCommand> ReadAllAsync(CancellationToken ct)
    {
        return _channel.Reader.ReadAllAsync(ct);
    }
}
