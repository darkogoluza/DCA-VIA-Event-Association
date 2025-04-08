using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Events.Values;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Invitations.Entities;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.VeaEventPersistence;

public static class VeaEventEntityConfiguration
{
    public static void ConfigureVeaEvent(EntityTypeBuilder<VeaEvent> entityBuilder)
    {
        entityBuilder.HasKey(veaEvent => veaEvent.VeaEventId);

        entityBuilder // PK
            .Property(veaEvent => veaEvent.VeaEventId)
            .HasConversion(
                veaEvent => veaEvent.Id,
                dbValue => VeaEventId.FromGuid(dbValue)
            )
            .HasColumnName("id");

        // Primitive Field
        entityBuilder
            .Property<bool?>("_visibility")
            .HasColumnName("visibility");
        entityBuilder
            .Property<DateTime?>("_endDateTime")
            .HasColumnName("endDateTime");
        entityBuilder
            .Property<DateTime?>("_startDateTime")
            .HasColumnName("startDateTime");

        // Nullable Value Objects
        entityBuilder
            .OwnsOne<Title>("_title")
            .Property(title => title.Value)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("title");
        entityBuilder
            .OwnsOne<Description>("_description")
            .Property(description => description.Value)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("description");
        entityBuilder
            .OwnsOne<MaxNoOfGuests>("_maxNoOfGuests")
            .Property(maxNoOfGuests => maxNoOfGuests.Value)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("maxNoOfGuests");

        // Enumeration class
        entityBuilder.ComplexProperty<EventStatusType>("_eventStatusType",
            propBuilder =>
            {
                propBuilder.Property("backingValue")
                    .HasColumnName("eventStatusType");
            }
        );

        // Single nested Entity
        entityBuilder
            .HasOne<Location>("_location")
            .WithOne()
            .HasForeignKey<Location>("locationId")
            .OnDelete(DeleteBehavior.Cascade);

        // List of entities
        entityBuilder
            .HasMany<Guest>("_guests")
            .WithOne()
            .HasForeignKey("eventId")
            .OnDelete(DeleteBehavior.Cascade);
        entityBuilder
            .HasMany<Invitation>("_invitations")
            .WithOne()
            .HasForeignKey("eventId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
