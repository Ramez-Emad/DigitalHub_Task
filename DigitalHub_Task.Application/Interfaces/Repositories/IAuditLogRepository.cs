using DigitalHub_Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.Interfaces.Repositories;

public interface IAuditLogRepository
{
    public void AddAuditLog(AuditLog auditLog);

    public Task SaveChangesAsync(CancellationToken ct);
}
