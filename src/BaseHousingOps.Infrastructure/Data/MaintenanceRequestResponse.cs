namespace BaseHousingOps.Infrastructure.Data;

public sealed record MaintenanceRequestResponse(
    int Id,
    int UnitId,
    int ResidentId,
    string Description,
    string? Category,
    string? Urgency,
    string Status,
    DateTime CreatedAt);