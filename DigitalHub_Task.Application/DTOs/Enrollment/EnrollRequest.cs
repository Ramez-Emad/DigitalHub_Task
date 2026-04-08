using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.DTOs.Enrollment;

public record EnrollRequest(int UserId , int CourseId);
