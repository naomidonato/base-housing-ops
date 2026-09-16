using BaseHousingOps.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BaseHousingOps.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
           .UseSnakeCaseNamingConvention());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapPost("/maintenance-requests", async (CreateMaintenanceRequestDto dto, AppDbContext db) =>
{
    var request = new MaintenanceRequest(dto.UnitId, dto.ResidentId, dto.Description);

    db.MaintenanceRequests.Add(request);
    await db.SaveChangesAsync();

    return Results.Created($"/maintenance-requests/{request.Id}", request);
});

app.Run();

record CreateMaintenanceRequestDto(int UnitId, int ResidentId, string Description);