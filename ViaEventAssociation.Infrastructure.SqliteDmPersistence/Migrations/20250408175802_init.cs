using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViaEventAssociation.Infrastructure.SqliteDmPersistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    endDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    startDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    visibility = table.Column<bool>(type: "INTEGER", nullable: true),
                    eventStatusType = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    maxNoOfGuests = table.Column<int>(type: "INTEGER", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    profilePictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    eventId = table.Column<Guid>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    firstName = table.Column<string>(type: "TEXT", nullable: false),
                    lastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.id);
                    table.ForeignKey(
                        name: "FK_Guests_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    locationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.locationId);
                    table.ForeignKey(
                        name: "FK_Location_Events_locationId",
                        column: x => x.locationId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    eventId = table.Column<Guid>(type: "TEXT", nullable: true),
                    inviteeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    statusType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invitations_Events_eventId",
                        column: x => x.eventId,
                        principalTable: "Events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Guests_inviteeId",
                        column: x => x.inviteeId,
                        principalTable: "Guests",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "RequestToJoins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    invitorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    veaEventId = table.Column<Guid>(type: "TEXT", nullable: true),
                    reason = table.Column<string>(type: "TEXT", nullable: false),
                    statusType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToJoins", x => x.id);
                    table.ForeignKey(
                        name: "FK_RequestToJoins_Events_veaEventId",
                        column: x => x.veaEventId,
                        principalTable: "Events",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_RequestToJoins_Guests_invitorId",
                        column: x => x.invitorId,
                        principalTable: "Guests",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_eventId",
                table: "Guests",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_eventId",
                table: "Invitations",
                column: "eventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_inviteeId",
                table: "Invitations",
                column: "inviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToJoins_invitorId",
                table: "RequestToJoins",
                column: "invitorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToJoins_veaEventId",
                table: "RequestToJoins",
                column: "veaEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "RequestToJoins");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
