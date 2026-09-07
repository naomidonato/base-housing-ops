using BaseHousingOps.Domain.Enums;

namespace BaseHousingOps.Domain.Entities;

public class Technician : Person
{
    public Specialty Specialty { get; private set; }

    public Technician(string firstName, string lastName, string email, Specialty specialty)
        : base(firstName, lastName, email)
    {
        Specialty = specialty;
    }

    public bool CanHandle(RequestCategory category) =>
        Specialty == Specialty.General ||
        (Specialty == Specialty.Plumbing && category == RequestCategory.Plumbing) ||
        (Specialty == Specialty.Electrical && category == RequestCategory.Electrical) ||
        (Specialty == Specialty.Hvac && category == RequestCategory.Hvac);
}
