using Microsoft.EntityFrameworkCore;

namespace ViaEventAssociation.Infrastructure.EfcQueries.Models;

public partial class VeadatabaseProductionContext : DbContext
{
    public VeadatabaseProductionContext()
    {
    }

    public VeadatabaseProductionContext(DbContextOptions<VeadatabaseProductionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EfmigrationsLock> EfmigrationsLocks { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Invitation> Invitations { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<RequestToJoin> RequestToJoins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source = C:\\\\\\\\Users\\\\\\\\Darko\\\\\\\\Desktop\\\\\\\\VIA\\\\\\\\Semester 7\\\\\\\\DCA\\\\\\\\VIAEventAssociation\\\\\\\\ViaEventAssociation.Infrastructure.SqliteDmPersistence\\\\\\\\VEADatabaseProduction.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EfmigrationsLock>(entity =>
        {
            entity.ToTable("__EFMigrationsLock");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDateTime).HasColumnName("endDateTime");
            entity.Property(e => e.EventStatusType).HasColumnName("eventStatusType");
            entity.Property(e => e.MaxNoOfGuests).HasColumnName("maxNoOfGuests");
            entity.Property(e => e.StartDateTime).HasColumnName("startDateTime");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Visibility).HasColumnName("visibility");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasIndex(e => e.EventId, "IX_Guests_eventId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EventId).HasColumnName("eventId");
            entity.Property(e => e.FirstName).HasColumnName("firstName");
            entity.Property(e => e.LastName).HasColumnName("lastName");
            entity.Property(e => e.ProfilePictureUrl).HasColumnName("profilePictureUrl");

            entity.HasOne(d => d.Event).WithMany(p => p.Guests)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Invitation>(entity =>
        {
            entity.HasIndex(e => e.EventId, "IX_Invitations_eventId");

            entity.HasIndex(e => e.InviteeId, "IX_Invitations_inviteeId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EventId).HasColumnName("eventId");
            entity.Property(e => e.InviteeId).HasColumnName("inviteeId");
            entity.Property(e => e.StatusType).HasColumnName("statusType");

            entity.HasOne(d => d.Event).WithMany(p => p.Invitations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Invitee).WithMany(p => p.Invitations).HasForeignKey(d => d.InviteeId);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasColumnName("locationId");

            entity.HasOne(d => d.LocationNavigation).WithOne(p => p.Location).HasForeignKey<Location>(d => d.LocationId);
        });

        modelBuilder.Entity<RequestToJoin>(entity =>
        {
            entity.HasIndex(e => e.InvitorId, "IX_RequestToJoins_invitorId");

            entity.HasIndex(e => e.VeaEventId, "IX_RequestToJoins_veaEventId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InvitorId).HasColumnName("invitorId");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.StatusType).HasColumnName("statusType");
            entity.Property(e => e.VeaEventId).HasColumnName("veaEventId");

            entity.HasOne(d => d.Invitor).WithMany(p => p.RequestToJoins).HasForeignKey(d => d.InvitorId);

            entity.HasOne(d => d.VeaEvent).WithMany(p => p.RequestToJoins).HasForeignKey(d => d.VeaEventId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
