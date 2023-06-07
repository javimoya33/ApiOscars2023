using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeliculas.Migrations
{
    /// <inheritdoc />
    public partial class Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            /*migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b07b45d-b55a-4fbb-9a20-7d37fc35e322",
                column: "ConcurrencyStamp",
                value: "3e986545-8eac-4863-b08e-fdecb956c35d");*/

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51969e84-2adf-4149-901f-c863e06854a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7f52103-5155-45f7-86c2-f9ffd6fade78", "AQAAAAEAACcQAAAAEEcez1KF7QC581DKaPjnWQknMnA9A8aK8nzQeF/C65LVeUI7ws2y2ugyhjF/4hIoEA==", "c98a64f8-e4ba-4a47-99a0-ac97600d4469" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PeliculaId",
                table: "Reviews",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            /*migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b07b45d-b55a-4fbb-9a20-7d37fc35e322",
                column: "ConcurrencyStamp",
                value: "c4a7fab0-0711-4185-a520-a2cd82ca5a58");*/

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51969e84-2adf-4149-901f-c863e06854a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea126836-5224-4f2f-ad57-28b6e7881368", "AQAAAAEAACcQAAAAEE4003bzMqD+qrfNefQAJ2+A882U+multRjfjpY8BtUhnqxuewkY7qhTU6gXhYsDog==", "7be79b7d-b667-492f-93fa-04892c9dad7b" });
        }
    }
}
