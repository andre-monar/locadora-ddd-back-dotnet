using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenomearTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ALOCACAO_Cliente_IdCliente",
                table: "TB_ALOCACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ALOCACAO_TB_CAR_IdCarro",
                table: "TB_ALOCACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_CAR_TB_CATEGORIA_CARRO_CategoriaId",
                table: "TB_CAR");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CAR",
                table: "TB_CAR");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "TB_CAR",
                newName: "TB_CARRO");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "TB_CLIENTE");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CAR_CategoriaId",
                table: "TB_CARRO",
                newName: "IX_TB_CARRO_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CAR_CAR_PLACA",
                table: "TB_CARRO",
                newName: "IX_TB_CARRO_CAR_PLACA");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_CLI_EMAIL",
                table: "TB_CLIENTE",
                newName: "IX_TB_CLIENTE_CLI_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_CLI_CPF",
                table: "TB_CLIENTE",
                newName: "IX_TB_CLIENTE_CLI_CPF");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CARRO",
                table: "TB_CARRO",
                column: "CAR_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CLIENTE",
                table: "TB_CLIENTE",
                column: "CLI_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_TB_CARRO_IdCarro",
                table: "TB_ALOCACAO",
                column: "IdCarro",
                principalTable: "TB_CARRO",
                principalColumn: "CAR_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_TB_CLIENTE_IdCliente",
                table: "TB_ALOCACAO",
                column: "IdCliente",
                principalTable: "TB_CLIENTE",
                principalColumn: "CLI_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CARRO_TB_CATEGORIA_CARRO_CategoriaId",
                table: "TB_CARRO",
                column: "CategoriaId",
                principalTable: "TB_CATEGORIA_CARRO",
                principalColumn: "CAT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ALOCACAO_TB_CARRO_IdCarro",
                table: "TB_ALOCACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_ALOCACAO_TB_CLIENTE_IdCliente",
                table: "TB_ALOCACAO");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_CARRO_TB_CATEGORIA_CARRO_CategoriaId",
                table: "TB_CARRO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CLIENTE",
                table: "TB_CLIENTE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CARRO",
                table: "TB_CARRO");

            migrationBuilder.RenameTable(
                name: "TB_CLIENTE",
                newName: "Cliente");

            migrationBuilder.RenameTable(
                name: "TB_CARRO",
                newName: "TB_CAR");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CLIENTE_CLI_EMAIL",
                table: "Cliente",
                newName: "IX_Cliente_CLI_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CLIENTE_CLI_CPF",
                table: "Cliente",
                newName: "IX_Cliente_CLI_CPF");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CARRO_CategoriaId",
                table: "TB_CAR",
                newName: "IX_TB_CAR_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CARRO_CAR_PLACA",
                table: "TB_CAR",
                newName: "IX_TB_CAR_CAR_PLACA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "CLI_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CAR",
                table: "TB_CAR",
                column: "CAR_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_Cliente_IdCliente",
                table: "TB_ALOCACAO",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "CLI_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_TB_CAR_IdCarro",
                table: "TB_ALOCACAO",
                column: "IdCarro",
                principalTable: "TB_CAR",
                principalColumn: "CAR_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CAR_TB_CATEGORIA_CARRO_CategoriaId",
                table: "TB_CAR",
                column: "CategoriaId",
                principalTable: "TB_CATEGORIA_CARRO",
                principalColumn: "CAT_ID");
        }
    }
}
