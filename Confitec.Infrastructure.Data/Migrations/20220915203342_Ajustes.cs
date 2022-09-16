using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Confitec.Infrastructure.Data.Migrations
{
    public partial class Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HistoricoEscolarId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EscolaridadeId",
                table: "Usuario",
                column: "EscolaridadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Escolaridades_EscolaridadeId",
                table: "Usuario",
                column: "EscolaridadeId",
                principalTable: "Escolaridades",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Escolaridades_EscolaridadeId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_EscolaridadeId",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "HistoricoEscolarId",
                table: "Usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
