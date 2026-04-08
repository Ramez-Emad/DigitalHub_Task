using DigitalHub_Task.Application.CQRS.Commands.AuditLog;
using DigitalHub_Task.Application.DTOs.Enrollment;
using DigitalHub_Task.Application.Interfaces;
using DigitalHub_Task.Application.Interfaces.Repositories;
using MediatR;
using System.Text.Json;
namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;

public class EnrollCommandHandler(IEnrollmentRepository _repo, IMediator _mediator) : IRequestHandler<EnrollCommand, EnrollResponse>
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

        await _mediator.Publish(new EnrollmentCreatedEvent(
            request.UserId,
            request.CourseId,
            enrollment.Id,
            nameof(enrollment)
        ));

        return response;

    }
}
