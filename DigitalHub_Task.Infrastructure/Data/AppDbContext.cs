using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> _options) : DbContext(_options)
{
}
