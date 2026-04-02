using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_PetShop.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoFKPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_clientes_ClientesId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_ClientesId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ClientesId",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ClienteId",
                table: "Pets",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_clientes_ClienteId",
                table: "Pets",
                column: "ClienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_clientes_ClienteId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_ClienteId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "ClientesId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ClientesId",
                table: "Pets",
                column: "ClientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_clientes_ClientesId",
                table: "Pets",
                column: "ClientesId",
                principalTable: "clientes",
                principalColumn: "Id");
        }
    }
}
