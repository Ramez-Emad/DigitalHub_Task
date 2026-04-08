using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using DigitalHub_Task.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;

public class EnrollmentCreatedEventHandler
    : INotificationHandler<EnrollmentCreatedEvent>
{
    private readonly IAuditLogQueue _auditQueue;

    public EnrollmentCreatedEventHandler(IAuditLogQueue auditQueue)
    {
        _auditQueue = auditQueue;
    }

    public async Task Handle(EnrollmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _auditQueue.EnqueueAsync(new AuditLogCommand(
            UserId: notification.UserId,
            Action: "EnrollCourse",
            EntityName: notification.EntityName,
            EntityId: notification.EnrollmentId,
            CreatedAt: DateTime.UtcNow,
            Metadata: $"courseId : {notification.CourseId} , userId : {notification.UserId}"
        ));
    }
}