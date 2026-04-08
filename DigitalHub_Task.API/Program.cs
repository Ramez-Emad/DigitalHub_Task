using DigitalHub_Task.Application;
using DigitalHub_Task.Application.Interfaces;
using DigitalHub_Task.Application.Interfaces.Repositories;
using DigitalHub_Task.Infrastructure.BackgroundServices;
using DigitalHub_Task.Infrastructure.Data;
using DigitalHub_Task.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

#region Swagger


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
                                        options.SwaggerDoc("v1", new OpenApiInfo
                                        {
                                            Version = "v1",
                                            Title = "DigitalHubTask",
                                        }
                                        )
                              );

#endregion

#region Add AppDbContext

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
  throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Repositories

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();

#endregion

#region MediatR

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ApplicationLayerProjectReference).Assembly));

#endregion


#region Background Service & Audit Queue

builder.Services.AddSingleton<AuditLogQueue>();
builder.Services.AddSingleton<IAuditLogQueue>(sp => sp.GetRequiredService<AuditLogQueue>());
builder.Services.AddHostedService<AuditLogWorker>(); 

#endregion


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
