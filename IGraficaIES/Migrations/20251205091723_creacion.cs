using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IGraficaIES.Migrations
{
    /// <inheritdoc />
    public partial class creacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfesoresFuncionarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnyoIngresoCuerpo = table.Column<int>(type: "int", nullable: false),
                    DestinoDefinitivo = table.Column<bool>(type: "bit", nullable: false),
                    TipoMedico = table.Column<long>(type: "bigint", nullable: false),
                    RutaFoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoProfesor = table.Column<long>(type: "bigint", nullable: false),
                    Materia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoresFuncionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfesoresExtendidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estatura = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<int>(type: "int", nullable: false),
                    ProfesorFuncionarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ECivil = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoresExtendidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesoresExtendidos_ProfesoresFuncionarios_ProfesorFuncionarioId",
                        column: x => x.ProfesorFuncionarioId,
                        principalTable: "ProfesoresFuncionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresExtendidos_ProfesorFuncionarioId",
                table: "ProfesoresExtendidos",
                column: "ProfesorFuncionarioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfesoresExtendidos");

            migrationBuilder.DropTable(
                name: "ProfesoresFuncionarios");
        }
    }
}
