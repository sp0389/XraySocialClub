using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XraySocialClub.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMemberUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_LP_MemberId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_SP_MemberId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LP_MemberId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LP_MemberId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "SP_MemberId",
                table: "Payments",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_SP_MemberId",
                table: "Payments",
                newName: "IX_Payments_MemberId");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_MemberId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Payments",
                newName: "SP_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_MemberId",
                table: "Payments",
                newName: "IX_Payments_SP_MemberId");

            migrationBuilder.AlterColumn<string>(
                name: "SP_MemberId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LP_MemberId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LP_MemberId",
                table: "Payments",
                column: "LP_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_LP_MemberId",
                table: "Payments",
                column: "LP_MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_SP_MemberId",
                table: "Payments",
                column: "SP_MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
