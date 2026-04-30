using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntegridadeReferencial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_TB_CARRO_CategoriaId",
                table: "TB_CARRO");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "TB_CARRO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CARRO_IdCategoria",
                table: "TB_CARRO",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_TB_CARRO_IdCarro",
                table: "TB_ALOCACAO",
                column: "IdCarro",
                principalTable: "TB_CARRO",
                principalColumn: "CAR_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ALOCACAO_TB_CLIENTE_IdCliente",
                table: "TB_ALOCACAO",
                column: "IdCliente",
                principalTable: "TB_CLIENTE",
                principalColumn: "CLI_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CARRO_TB_CATEGORIA_CARRO_IdCategoria",
                table: "TB_CARRO",
                column: "IdCategoria",
                principalTable: "TB_CATEGORIA_CARRO",
                principalColumn: "CAT_ID",
                onDelete: ReferentialAction.Restrict);
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
                name: "FK_TB_CARRO_TB_CATEGORIA_CARRO_IdCategoria",
                table: "TB_CARRO");

            migrationBuilder.DropIndex(
                name: "IX_TB_CARRO_IdCategoria",
                table: "TB_CARRO");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "TB_CARRO",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_CARRO_CategoriaId",
                table: "TB_CARRO",
                column: "CategoriaId");

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
    }
}
