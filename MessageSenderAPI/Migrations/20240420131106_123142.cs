using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationMessageSender.API.Migrations
{
    /// <inheritdoc />
    public partial class _123142 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "NotificationsRequests",
                newName: "SentAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentAt",
                table: "NotificationsRequests",
                newName: "CreatedAt");
        }
    }
}
