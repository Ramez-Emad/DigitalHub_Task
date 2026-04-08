using DigitalHub_Task.Application.DTOs.Enrollment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.CQRS.Commands.Enrollment;

public record EnrollCommand(int UserId , int CourseId) : IRequest<EnrollResponse>;

