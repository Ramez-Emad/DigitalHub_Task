using DigitalHub_Task.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Application.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    public void AddEnrollment(Enrollment enrollment);

    public Task SaveChangesAsync(CancellationToken ct);
}
