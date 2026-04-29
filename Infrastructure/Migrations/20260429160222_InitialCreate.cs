using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CLI_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CLI_NOME = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CLI_CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CLI_DATA_NASCIMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CLI_CELULAR = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    CLI_EMAIL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CLI_CEP = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    CLI_ENDERECO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CLI_COMPLEMENTO_ENDERECO = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    CLI_ATIVO = table.Column<bool>(type: "boolean", nullable: false),
                    CLI_DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CLI_DATA_ALTERACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CLI_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CATEGORIA_CARRO",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CAT_NOME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CAT_DESCRICAO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CAT_VALOR_DIARIA = table.Column<decimal>(type: "numeric", nullable: false),
                    CAT_ATIVO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIA_CARRO", x => x.CAT_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_CAR",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "integer", nullable: false),
                    CAR_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CAR_MODELO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CAR_MARCA = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CAR_PLACA = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    CAR_ANO = table.Column<int>(type: "integer", nullable: false),
                    CAR_COR = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CAR_IMAGEM_URL = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CategoriaId = table.Column<int>(type: "integer", nullable: true),
                    CAR_ATIVO = table.Column<bool>(type: "boolean", nullable: false),
                    CAR_DISPONIVEL = table.Column<bool>(type: "boolean", nullable: false),
                    CAR_DATA_ALTERACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CAR", x => x.CAR_ID);
                    table.ForeignKey(
                        name: "FK_TB_CAR_TB_CATEGORIA_CARRO_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "TB_CATEGORIA_CARRO",
                        principalColumn: "CAT_ID");
                });

            migrationBuilder.CreateTable(
                name: "TB_ALOCACAO",
                columns: table => new
                {
                    IdCarro = table.Column<int>(type: "integer", nullable: false),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    ALO_ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ALO_STATUS = table.Column<int>(type: "integer", nullable: false),
                    ALO_DATA_RETIRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ALO_DATA_DEVOLUCAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ALO_DATA_PREVISTA_DEVOLUCAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ALO_VALOR_TOTAL = table.Column<decimal>(type: "numeric", nullable: false),
                    ALO_DATA_ALTERACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CAR_DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALOCACAO", x => x.ALO_ID);
                    table.ForeignKey(
                        name: "FK_TB_ALOCACAO_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "CLI_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALOCACAO_TB_CAR_IdCarro",
                        column: x => x.IdCarro,
                        principalTable: "TB_CAR",
                        principalColumn: "CAR_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CLI_CPF",
                table: "Cliente",
                column: "CLI_CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CLI_EMAIL",
                table: "Cliente",
                column: "CLI_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALOCACAO_IdCarro",
                table: "TB_ALOCACAO",
                column: "IdCarro");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALOCACAO_IdCliente",
                table: "TB_ALOCACAO",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CAR_CAR_PLACA",
                table: "TB_CAR",
                column: "CAR_PLACA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_CAR_CategoriaId",
                table: "TB_CAR",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CATEGORIA_CARRO_CAT_NOME",
                table: "TB_CATEGORIA_CARRO",
                column: "CAT_NOME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ALOCACAO");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "TB_CAR");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIA_CARRO");
        }
    }
}
