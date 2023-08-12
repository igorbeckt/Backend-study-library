using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarSys.Application.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RESERVA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RESERVA_STATUS = table.Column<int>(type: "int", nullable: false),
                    DATA_RESERVA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VALOR_LOCACAO = table.Column<double>(type: "float", nullable: false),
                    DATA_RETIRADA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DATA_ENTREGA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLIENTE_STATUS = table.Column<int>(type: "int", nullable: false),
                    NOME_COMPLETO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RG = table.Column<long>(type: "bigint", nullable: false),
                    CPF = table.Column<long>(type: "bigint", nullable: false),
                    RESERVA_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REFERENCE_CLIENTE_RESERVA",
                        column: x => x.RESERVA_ID,
                        principalTable: "RESERVA",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "VEICULO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VEICULO_STATUS = table.Column<int>(type: "int", nullable: false),
                    PLACA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MARCA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MODELO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANO_FABRICACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantidadePortas = table.Column<int>(type: "int", nullable: false),
                    COR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AUTOMATICO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RESERVA_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REFERENCE_VEICULO_RESERVA",
                        column: x => x.RESERVA_ID,
                        principalTable: "RESERVA",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTE_RESERVA_ID",
                table: "CLIENTE",
                column: "RESERVA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_VEICULO_RESERVA_ID",
                table: "VEICULO",
                column: "RESERVA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "VEICULO");

            migrationBuilder.DropTable(
                name: "RESERVA");
        }
    }
}
