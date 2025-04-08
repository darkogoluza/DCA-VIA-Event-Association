using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Entities;
using VIAEventAssociation.Core.Domain.Aggregates.Guests.Values;
using VIAEventAssociation.Core.Domain.Common.Values;

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.GuestPersistence;

public static class GuestEntityConfiguration
{
    public static void ConfigureGuest(EntityTypeBuilder<Guest> entityBuilder)
    {
        entityBuilder.HasKey(guest => guest.GuestId);

        entityBuilder // PK
            .Property(guest => guest.GuestId)
            .HasConversion(
                guestId => guestId.Id,
                dbValue => GuestId.FromGuid(dbValue)
            )
            .HasColumnName("id");

        // Non-nullable fields
        entityBuilder.ComplexProperty<FirstName>(
            "_firstName",
            propBuilder =>
            {
                propBuilder.Property(firstName => firstName.Value)
                    .HasColumnName("firstName");
            }
        );
        entityBuilder.ComplexProperty<LastName>(
            "_lastName",
            propBuilder =>
            {
                propBuilder.Property(lastName => lastName.Value)
                    .HasColumnName("lastName");
            }
        );
        entityBuilder.ComplexProperty<Email>(
            "_email",
            propBuilder =>
            {
                propBuilder.Property(email => email.Value)
                    .HasColumnName("email");
            }
        );

        // Primitive Field
        entityBuilder
            .Property<Uri>("_profilePictureUrl")
            .HasColumnName("profilePictureUrl");
    }
}
