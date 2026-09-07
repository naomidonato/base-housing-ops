using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Domain.Entities;

public class LeaseResident
{
    public int ResidentId { get; private set; }
    public LeaseRole Role { get; private set; }

    public LeaseResident(int residentId, LeaseRole role)
    {
        ResidentId = residentId;
        Role = role;
    }
}
