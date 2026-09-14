using BaseHousingOps.Domain.Entities;
using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Tests;

public class TechnicianTests
{
    [Fact]
    public void GeneralTechnician_ShouldHandleAnyRequest()
    {
        // Arrange
        var technician = new Technician(
            "Mike",
            "Smith",
            "mike@example.com",
            Specialty.General);

        // Act
        var result = technician.CanHandle(RequestCategory.Plumbing);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlumbingTechnician_ShouldHandlePlumbingRequest()
    {
        // Arrange
        var technician = new Technician(
            "Mike",
            "Smith",
            "mike@example.com",
            Specialty.Plumbing);

        // Act
        var result = technician.CanHandle(RequestCategory.Plumbing);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlumbingTechnician_ShouldNotHandleElectricalRequest()
    {
        // Arrange
        var technician = new Technician(
            "Mike",
            "Smith",
            "mike@example.com",
            Specialty.Plumbing);

        // Act
        var result = technician.CanHandle(RequestCategory.Electrical);

        // Assert
        Assert.False(result);
    }
}