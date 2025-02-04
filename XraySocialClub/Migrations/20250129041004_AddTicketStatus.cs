using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XraySocialClub.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketStatus",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketStatus",
                table: "Tickets");
        }
    }
}
