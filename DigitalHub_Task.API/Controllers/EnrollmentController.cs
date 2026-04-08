using DigitalHub_Task.Application.CQRS.Commands.Enrollment;
using DigitalHub_Task.Application.DTOs.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub_Task.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentController(ISender _sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] EnrollRequest request, CancellationToken ct)
    {
        var response = await _sender.Send(new EnrollCommand(request.UserId, request.CourseId), ct);
        return Ok(response);
    }
}
