using System.Reflection;
using BaseHousingOps.Domain.Entities;

namespace BaseHousingOps.Tests;

//Testing that residents can be created.
//Your test's expectation does not equal the application's actual behavior.
public class ResidentTests
{
    [Fact]
    public void Constructor_ShouldCreateResident()
    {
        // Arange
        var resident = new Resident (
            "John",
            "Smith",
            "john@example.com",
            "Navy");
        
        // Assert
        Assert.Equal("John", resident.FirstName); //Failure: Strings differ, means the assertion failed
        Assert.Equal("Smith", resident.LastName);
        Assert.Equal("john@example.com", resident.Email);
        Assert.Equal("Navy", resident.BranchOfService);
    }

    //We are saying "I expect this code to throw an ArgumentExeption."
    //Create invalid Resident -> Does it throw ArgumentException? -> YES -> TEST PASS
    //If Resident does not throw an exception: Create invalid Resident -> No exception -> Test FAIL
    [Fact]
    public void Constructor_ShouldRejectEmptyFirstName()
    {
        //Act & Assert

        //I EXPECT an ArgumentException.
        Assert.Throws<ArgumentException>(() => //run this code
            new Resident(
                "",
                "Smith",
                "john@example.com",
                "Navy"));
    }

    [Fact]

    public void FullName_ShouldCombineFirstAndLastName()
    {
        //Arange
        var resident = new Resident (
            "John",
            "Smith",
            "john@example.com",
            "Navy");
        
        //Act
        var fullName = resident.FullName;

        //Assert
        Assert.Equal("John Smith", fullName);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyLastName()
    {
        Assert.Throws<ArgumentException>(() =>
            new Resident(
                "John",
                "",
                "john@example.com",
                "Navy"));
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyEmail()
    {
        Assert.Throws<ArgumentException>(() =>
            new Resident(
                "John",
                "Smith",
                "",
                "Navy"));
    }

    [Fact]
    public void Constructor_ShouldRejectInvalidEmail()
    {
        Assert.Throws<ArgumentException>(() =>
            new Resident(
                "John",
                "Smith",
                "john.example.com",
                "Navy"));
    }


    
}
