using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Domain.Entities;

public class MaintenanceRequest
{
    public int Id { get; private set; }
    public int UnitId { get; private set; }
    public int ResidentId { get; private set; }
    public string Description { get; private set; }
    public RequestCategory? Category { get; private set; }
    public RequestUrgency? Urgency { get; private set; }
    public RequestStatus Status { get; private set; } = RequestStatus.Submitted;

    public MaintenanceRequest(int unitId, int residentId, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        UnitId = unitId;
        ResidentId = residentId;
        Description = description;
    }

    public void Triage(RequestCategory category, RequestUrgency urgency)
    {
        if (Status != RequestStatus.Submitted)
            throw new InvalidOperationException($"Cannot triage a request in '{Status}' status.");

        Category = category;
        Urgency = urgency;
        Status = RequestStatus.Triaged;
    }

    public void Resolve()
    {
        if (Status is RequestStatus.Resolved or RequestStatus.Closed)
            throw new InvalidOperationException($"Request is already {Status}.");
        Status = RequestStatus.Resolved;
    }

    public void Close()
    {
        if (Status != RequestStatus.Resolved)
            throw new InvalidOperationException("Only a resolved request can be closed.");
        Status = RequestStatus.Closed;
    }
}
