using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSOL.Migrations
{
    /// <inheritdoc />
    public partial class hotelsol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioBase",
                table: "Habitaciones",
                newName: "Precio");

            migrationBuilder.RenameColumn(
                name: "PrecioBase",
                table: "Alojamientos",
                newName: "Precio");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Servicios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReservaServicios",
                columns: table => new
                {
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaServicios", x => new { x.ReservaId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_ReservaServicios_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicios_ServicioId",
                table: "ReservaServicios",
                column: "ServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaServicios");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Servicios");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Habitaciones",
                newName: "PrecioBase");

            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Alojamientos",
                newName: "PrecioBase");
        }
    }
}
