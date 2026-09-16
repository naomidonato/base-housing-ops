using BaseHousingOps.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseHousingOps.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Resident> Residents => Set<Resident>();
    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<Lease> Leases => Set<Lease>();
    public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Person is abstract and, per schema.sql, has NO table of its own —
        // "resident" and "technician" are two completely separate tables with
        // no shared "person" table and no discriminator column. TPC (Table-
        // Per-Concrete-Type) tells EF exactly that: give each concrete
        // subclass its own independent table, don't invent a shared one.
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();

        modelBuilder.Entity<Technician>().Property(t => t.Specialty).HasConversion<string>();

        modelBuilder.Entity<Lease>(e =>
        {
            e.Metadata.FindNavigation(nameof(Lease.Residents))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            e.HasOne<Unit>().WithMany().HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            e.OwnsMany(l => l.Residents, lr =>
            {
                lr.WithOwner().HasForeignKey("LeaseId");
                lr.HasKey("LeaseId", nameof(LeaseResident.ResidentId));
                lr.Property(x => x.Role).HasConversion<string>();

                // The owned-entity FK to "leases" was already generated automatically
                // (via WithOwner above). This second FK, to "residents", was missing
                // entirely — same root cause as everywhere else.
                lr.HasOne<Resident>().WithMany().HasForeignKey(x => x.ResidentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        });

        modelBuilder.Entity<Unit>()
            .HasOne<Property>().WithMany().HasForeignKey(u => u.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceRequest>(e =>
        {
            e.Property(m => m.Category).HasConversion<string>();
            e.Property(m => m.Urgency).HasConversion<string>();
            e.Property(m => m.Status).HasConversion<string>();

            e.HasOne<Unit>().WithMany().HasForeignKey(m => m.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Resident>().WithMany().HasForeignKey(m => m.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<WorkOrder>(e =>
        {
            e.Property(w => w.Status).HasConversion<string>();

            e.HasOne<MaintenanceRequest>().WithMany().HasForeignKey(w => w.RequestId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Technician>().WithMany().HasForeignKey(w => w.TechnicianId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}