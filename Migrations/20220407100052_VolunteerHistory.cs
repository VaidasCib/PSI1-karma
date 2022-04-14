using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Karma.Migrations
{
    public partial class VolunteerHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "CharityEventVolunteer");

            migrationBuilder.DropColumn(
                name: "Participated",
                table: "CharityEventVolunteer");

            migrationBuilder.CreateTable(
                name: "CharityEventVolunteers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharityEventId = table.Column<Guid>(type: "TEXT", nullable: true),
                    VolunteerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Participated = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityEventVolunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityEventVolunteers_Events_CharityEventId",
                        column: x => x.CharityEventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CharityEventVolunteers_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventVolunteers_CharityEventId",
                table: "CharityEventVolunteers",
                column: "CharityEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityEventVolunteers_VolunteerId",
                table: "CharityEventVolunteers",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharityEventVolunteers");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "CharityEventVolunteer",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Participated",
                table: "CharityEventVolunteer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
