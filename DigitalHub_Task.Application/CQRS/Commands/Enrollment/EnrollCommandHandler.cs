using DigitalHub_Task.Application.DTOs.Enrollment;
using DigitalHub_Task.Application.Interfaces.Repositories;
using MediatR;
namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;

public class EnrollCommandHandler(IEnrollmentRepository _repo) : IRequestHandler<EnrollCommand, EnrollResponse>
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

        return response;

    }
}
