using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeliculas.Migrations
{
    /// <inheritdoc />
    public partial class AdminData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7b07b45d-b55a-4fbb-9a20-7d37fc35e322", "c4a7fab0-0711-4185-a520-a2cd82ca5a58", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "51969e84-2adf-4149-901f-c863e06854a8", 0, "ea126836-5224-4f2f-ad57-28b6e7881368", "javimoya33@gmail.com", false, false, null, "javimoya33@gmail.com", "javimoya33@gmail.com", "AQAAAAEAACcQAAAAEE4003bzMqD+qrfNefQAJ2+A882U+multRjfjpY8BtUhnqxuewkY7qhTU6gXhYsDog==", null, false, "7be79b7d-b667-492f-93fa-04892c9dad7b", false, "javimoya33@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "51969e84-2adf-4149-901f-c863e06854a8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b07b45d-b55a-4fbb-9a20-7d37fc35e322");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51969e84-2adf-4149-901f-c863e06854a8");
        }
    }
}
