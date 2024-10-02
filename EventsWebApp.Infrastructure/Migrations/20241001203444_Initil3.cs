using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initil3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SocialEvents",
                newName: "EventName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "SocialEvents",
                newName: "Name");
        }
    }
}
