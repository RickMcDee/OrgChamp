using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgChamp.Migrations
{
    /// <inheritdoc />
    public partial class IntroducingTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "UserManagement",
                table: "UserDetails");

            migrationBuilder.EnsureSchema(
                name: "Organization");

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "Organization",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "UserManagement",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                schema: "Organization",
                columns: table => new
                {
                    TeamMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamMemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.TeamMemberId);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Organization",
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "UserManagement",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                schema: "Organization",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                schema: "Organization",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatedById",
                schema: "Organization",
                table: "Teams",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "Organization");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "UserManagement",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
