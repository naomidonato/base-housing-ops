namespace BaseHousingOps.Infrastructure.Data;

public sealed record CreatedMaintenanceRequest(
    int Id,
    int UnitId,
    int ResidentId,
    string Description,
    string? Category,
    string? Urgency,
    string Status);