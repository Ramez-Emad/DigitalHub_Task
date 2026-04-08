using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using DigitalHub_Task.Application.DTOs.Enrollment;
using DigitalHub_Task.Application.Interfaces;
using DigitalHub_Task.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;
namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;

public class EnrollCommandHandler(IEnrollmentRepository _repo , IAuditLogQueue _auditQueue) : IRequestHandler<EnrollCommand, EnrollResponse>
{
    public async Task<EnrollResponse> Handle(EnrollCommand request, CancellationToken cancellationToken)
    {
        var enrollment = new Domain.Entities.Enrollment
        {
            UserId = request.UserId,
            CourseId = request.CourseId,
            EnrolledAt = DateTime.UtcNow
        };

        _repo.AddEnrollment(enrollment);
        await _repo.SaveChangesAsync(cancellationToken);

        var response = new EnrollResponse
        (
           enrollment.Id
        );

        await _auditQueue.EnqueueAsync(new AuditLogCommand(
            UserId: request.UserId,
            Action: "EnrollCourse",
            EntityName: nameof(enrollment),
            EntityId: enrollment.Id,
            CreatedAt: DateTime.UtcNow,
            Metadata : $"courseId : {request.CourseId} , userId : {request.UserId}"
        ));

        return response;

    }
}
