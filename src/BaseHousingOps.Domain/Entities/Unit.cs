namespace BaseHousingOps.Domain.Entities;

public class Unit
{
    public int Id { get; private set; }
    public int PropertyId { get; private set; }
    public string UnitNumber { get; private set; }
    public int Bedrooms { get; private set; }
    public bool IsVacant { get; private set; } = true;

    public Unit(int propertyId, string unitNumber, int bedrooms)
    {
        if (bedrooms < 0)
            throw new ArgumentException("Bedrooms cannot be negative.", nameof(bedrooms));

        PropertyId = propertyId;
        UnitNumber = unitNumber;
        Bedrooms = bedrooms;
    }

    public void MarkOccupied()
    {
        if (!IsVacant)
            throw new InvalidOperationException($"Unit {UnitNumber} is already occupied.");
        IsVacant = false;
    }

    public void MarkVacant() => IsVacant = true;
}
