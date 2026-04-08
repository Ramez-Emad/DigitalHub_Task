
using DigitalHub_Task.Application.Interfaces.Repositories;
using DigitalHub_Task.Domain.Entities;
using DigitalHub_Task.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Infrastructure.Repositories;

public class EnrollmentRepository(AppDbContext _db) : IEnrollmentRepository
{
    public void AddEnrollment(Enrollment enrollment) => _db.Enrollments.Add(enrollment);

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
