using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Domain.Entities;

public class Lease
{
    public int Id { get; private set; }
    public int UnitId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }

    private readonly List<LeaseResident> _residents = new();
    public IReadOnlyList<LeaseResident> Residents => _residents.AsReadOnly();

    public Lease(int unitId, DateOnly startDate)
    {
        UnitId = unitId;
        StartDate = startDate;
    }

    public void AddResident(int residentId, LeaseRole role)
    {
        if (role == LeaseRole.Primary && _residents.Any(r => r.Role == LeaseRole.Primary))
            throw new InvalidOperationException("This lease already has a primary resident.");

        if (_residents.Any(r => r.ResidentId == residentId))
            throw new InvalidOperationException("This resident is already on the lease.");

        _residents.Add(new LeaseResident(residentId, role));
    }

    public void End(DateOnly endDate)
    {
        if (endDate <= StartDate)
            throw new ArgumentException("End date must be after the start date.", nameof(endDate));
        EndDate = endDate;
    }
}
