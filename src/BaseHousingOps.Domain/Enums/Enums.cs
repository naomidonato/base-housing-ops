namespace BaseHousingOps.Domain.Enums;

public enum Specialty
{
    Plumbing,
    Electrical,
    Hvac,
    General
}

public enum RequestCategory
{
    Plumbing,
    Electrical,
    Hvac,
    General
}

public enum LeaseRole
{
    Primary,
    CoSigner
}

public enum RequestUrgency
{
    Emergency,
    Urgent,
    Routine
}

public enum RequestStatus
{
    Submitted,
    Triaged,
    InProgress,
    Resolved,
    Closed
}

public enum WorkOrderStatus
{
    Unassigned,
    Scheduled,
    Completed,
    Cancelled
}
