namespace BaseHousingOps.Domain.Entities;

public class Resident : Person
{
    public string? BranchOfService { get; private set; }

    public Resident(string firstName, string lastName, string email, string? branchOfService = null)
        : base(firstName, lastName, email)
    {
        BranchOfService = branchOfService;
    }
}
