using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationService.Migrations
{
    /// <inheritdoc />
    public partial class PersonGroupTableAndMigrationLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Groups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonGroups",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GroupRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroups", x => new { x.PersonId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_PersonGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGroups_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_GroupId",
                table: "PersonGroups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonGroups");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Groups");
        }
    }
}
