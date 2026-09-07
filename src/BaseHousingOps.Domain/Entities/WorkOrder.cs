using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Domain.Entities;

public class WorkOrder
{
    public int Id { get; private set; }
    public int RequestId { get; private set; }
    public int? TechnicianId { get; private set; }
    public WorkOrderStatus Status { get; private set; } = WorkOrderStatus.Unassigned;
    public DateOnly? ScheduledDate { get; private set; }
    public DateOnly? CompletedDate { get; private set; }

    public WorkOrder(int requestId)
    {
        RequestId = requestId;
    }

    public void AssignTechnician(int technicianId, DateOnly scheduledDate)
    {
        if (Status != WorkOrderStatus.Unassigned)
            throw new InvalidOperationException($"Cannot assign a technician to a work order in '{Status}' status.");

        TechnicianId = technicianId;
        ScheduledDate = scheduledDate;
        Status = WorkOrderStatus.Scheduled;
    }

    public void Complete(DateOnly completedDate)
    {
        if (Status != WorkOrderStatus.Scheduled)
            throw new InvalidOperationException("Only a scheduled work order can be completed.");

        CompletedDate = completedDate;
        Status = WorkOrderStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == WorkOrderStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed work order.");
        Status = WorkOrderStatus.Cancelled;
    }
}
