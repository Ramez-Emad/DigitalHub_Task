using DigitalHub_Task.Application.Interfaces.Repositories;
using DigitalHub_Task.Domain.Entities;
using DigitalHub_Task.Infrastructure.Data;

namespace DigitalHub_Task.Infrastructure.Repositories;



public class AuditLogRepository(AppDbContext _db) : IAuditLogRepository
{
    public void AddAuditLog(AuditLog auditLog) => _db.AuditLogs.Add(auditLog);
    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
