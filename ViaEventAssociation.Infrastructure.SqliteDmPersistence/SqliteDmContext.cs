using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Values;
using VIAEventAssociation.Core.Domain.Aggregates.RequestsToJoin.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.RequestsToJoin.Values;
using VIAEventAssociation.Core.Domain.Common.Values;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.GuestPersistence;
using ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence;

public class SqliteDmContext(DbContextOptions<SqliteDmContext> options) : DbContext(options)
{
    public DbSet<VeaEvent> Events => Set<VeaEvent>();
    public DbSet<Guest> Guests => Set<Guest>();
    public DbSet<Invitation> Invitations => Set<Invitation>();
    public DbSet<RequestToJoin> RequestToJoins => Set<RequestToJoin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqliteDmContext).Assembly);
        VeaEventEntityConfiguration.ConfigureVeaEvent(modelBuilder.Entity<VeaEvent>());
        GuestEntityConfiguration.ConfigureGuest(modelBuilder.Entity<Guest>());
        ConfigureInvitations(modelBuilder.Entity<Invitation>());
        ConfigureRequestsToJoin(modelBuilder.Entity<RequestToJoin>());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging();
    }

    private static void ConfigureRequestsToJoin(EntityTypeBuilder<RequestToJoin> entityBuilder)
    {
        entityBuilder.HasKey(rtj => rtj.RequestToJoinId);

        entityBuilder // PK
            .Property(rtj => rtj.RequestToJoinId)
            .HasConversion(
                rtj => rtj.Id,
                dbValue => RequestToJoinId.FromGuid(dbValue)
            )
            .HasColumnName("id");

        // Enumeration class
        entityBuilder.ComplexProperty<StatusType>("_statusType",
            propBuilder =>
            {
                propBuilder.Property("backingValue")
                    .HasColumnName("statusType");
            }
        );

        // Non-nullable fields
        entityBuilder.ComplexProperty<Reasion>(
            "_reason",
            propBuilder =>
            {
                propBuilder.Property(reason => reason.Value)
                    .HasColumnName("reason");
            }
        );

        // Strongly Typed Foreign Key
        entityBuilder.HasOne<Guest>()
            .WithMany()
            .HasForeignKey("invitorId");
        entityBuilder.HasOne<VeaEvent>()
            .WithMany()
            .HasForeignKey("veaEventId");
    }

    private static void ConfigureInvitations(EntityTypeBuilder<Invitation> entityBuilder)
    {
        entityBuilder.HasKey(invitation => invitation.InvitationId);

        entityBuilder // PK
            .Property(invitation => invitation.InvitationId)
            .HasConversion(
                invitationId => invitationId.Id,
                dbValue => InvitationId.FromGuid(dbValue)
            )
            .HasColumnName("id");

        // Enumeration class
        entityBuilder.ComplexProperty<StatusType>("_statusType",
            propBuilder =>
            {
                propBuilder.Property("backingValue")
                    .HasColumnName("statusType");
            }
        );

        // Strongly Typed Foreign Key
        entityBuilder.HasOne<Guest>()
            .WithMany()
            .HasForeignKey("inviteeId");
    }
}
