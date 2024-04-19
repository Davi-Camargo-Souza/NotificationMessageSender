using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationMessageSender.API.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "RequestTime",
                table: "NotificationsRequests",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "NotificationsRequests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "NotificationsRequests");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "NotificationsRequests",
                newName: "RequestTime");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
