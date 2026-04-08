using MediatR;

namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;


public record EnrollmentCreatedEvent(
    int UserId,
    int CourseId,
    int EnrollmentId,
    string EntityName
) : INotification;