using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DigitalHub_Task.Infrastructure.BackgroundServices;

public class AuditLogWorker(
    AuditLogQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<AuditLogWorker> logger
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var command in queue.ReadAllAsync(ct))
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var sender = scope.ServiceProvider
                    .GetRequiredService<ISender>();

                await sender.Send(command, ct);

                logger.LogInformation(
                    "Audit saved | Action: {Action} | Entity: {EntityName} | EntityId: {EntityId}",
                    command.Action,
                    command.EntityName,
                    command.EntityId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Failed to save audit | Action: {Action} | Entity: {EntityName}",
                    command.Action,
                    command.EntityName);
            }
        }
    }
}