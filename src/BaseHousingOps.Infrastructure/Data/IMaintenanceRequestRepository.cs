using BaseHousingOps.Domain.Entities;

namespace BaseHousingOps.Infrastructure.Data;

public interface IMaintenanceRequestRepository
{
    Task<CreatedMaintenanceRequest> CreateAsync(
        MaintenanceRequest request,
        CancellationToken cancellationToken = default);

    Task<MaintenanceRequestResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}