namespace BaseHousingOps.Domain.Entities;

public class Property
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }

    public Property(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Property name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address is required.", nameof(address));

        Name = name;
        Address = address;
    }
}
