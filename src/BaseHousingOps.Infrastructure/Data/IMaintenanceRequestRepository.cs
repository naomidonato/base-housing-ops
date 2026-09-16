using BaseHousingOps.Domain.Entities;

namespace BaseHousingOps.Infrastructure.Data;

public interface IMaintenanceRequestRepository
{
    Task<CreatedMaintenanceRequest> CreateAsync(
        MaintenanceRequest request,
        CancellationToken cancellationToken = default);
}