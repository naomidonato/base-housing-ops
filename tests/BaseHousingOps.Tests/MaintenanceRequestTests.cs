using BaseHousingOps.Domain.Entities;
using BaseHousingOps.Domain.Enums;
using Xunit;

namespace BaseHousingOps.Tests;

public class MaintenanceRequestTests
{
    [Fact]
    public void Triage_ShouldChangeSubmittedRequestToTriaged()
    {
        // Arrange
        var request = new MaintenanceRequest(
            1,
            2,
            "Fix the window curtain."
        );

        // Act
        request.Triage(
            RequestCategory.Plumbing,
            RequestUrgency.Routine);

        // Assert
        Assert.Equal(RequestStatus.Triaged, request.Status);
    }
}