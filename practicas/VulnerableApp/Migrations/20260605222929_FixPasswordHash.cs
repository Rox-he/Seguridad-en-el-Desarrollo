using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VulnerableApp.Migrations
{
    /// <inheritdoc />
    public partial class FixPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ1234");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ5678");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$5IJFbMBGrZCxhkXFOFaQqeUeABCDEFGHIJKLMNOPQRSTUVWXYZ9012");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$qBIZD/0Na4owG..vkJTUO.hJ4UzjqZR5J0JMOYFuFo5KqOPT1.EZ6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$HIStAwH0Dispk9F7N3fz0.kix2pPrDntwTsy99DR3BIPrqaFKDXtO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$xaoaMlHDI1zjidFRkK6s4OJOynP0HKyJg4fHxoV01h3dhHDQpfxD2");
        }
    }
}
