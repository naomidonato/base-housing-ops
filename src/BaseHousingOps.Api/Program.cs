using BaseHousingOps.Domain.Entities;
using BaseHousingOps.Infrastructure.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "The Default database connection string is missing.");

builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter()));

builder.Services.AddSingleton(
    NpgsqlDataSource.Create(connectionString));

builder.Services.AddScoped<
    IMaintenanceRequestRepository,
    MaintenanceRequestRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost(
    "/maintenance-requests",
    async (
        CreateMaintenanceRequestDto dto,
        IMaintenanceRequestRepository repository,
        CancellationToken cancellationToken) =>
    {
        var request = new MaintenanceRequest(
            dto.UnitId,
            dto.ResidentId,
            dto.Description);

        var created = await repository.CreateAsync(
            request,
            cancellationToken);

        return Results.Created(
            $"/maintenance-requests/{created.Id}",
            created);
    });

app.Run();

record CreateMaintenanceRequestDto(
    int UnitId,
    int ResidentId,
    string Description);