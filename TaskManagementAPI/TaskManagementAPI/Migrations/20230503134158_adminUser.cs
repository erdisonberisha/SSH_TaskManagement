using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class adminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "date_of_birth", "email", "name", "password_hash", "password_salt", "role", "username" },
                values: new object[] { 1000, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "admin admin", new byte[] { 73, 153, 176, 168, 5, 13, 189, 176, 182, 251, 77, 226, 120, 192, 111, 184, 150, 161, 162, 31, 210, 48, 84, 171, 76, 81, 111, 172, 103, 158, 117, 204, 120, 15, 45, 72, 206, 198, 66, 155, 80, 138, 115, 107, 16, 217, 165, 16, 55, 100, 195, 6, 84, 34, 98, 229, 225, 147, 94, 205, 193, 86, 178, 253 }, new byte[] { 47, 196, 224, 196, 141, 202, 85, 25, 159, 80, 174, 30, 191, 230, 146, 159, 75, 239, 149, 246, 85, 138, 26, 157, 104, 25, 49, 247, 226, 89, 216, 121, 33, 181, 183, 179, 77, 65, 131, 253, 136, 219, 198, 164, 213, 3, 53, 202, 249, 31, 81, 153, 34, 134, 249, 184, 239, 161, 166, 206, 94, 173, 82, 71, 82, 137, 71, 251, 106, 15, 255, 96, 161, 41, 74, 158, 154, 115, 124, 121, 213, 35, 108, 127, 160, 240, 239, 93, 105, 105, 253, 150, 213, 210, 63, 110, 150, 217, 169, 242, 9, 172, 216, 213, 243, 54, 160, 32, 192, 89, 197, 171, 113, 76, 167, 53, 126, 77, 183, 84, 84, 192, 113, 164, 92, 143, 4, 247 }, "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1000);
        }
    }
}
