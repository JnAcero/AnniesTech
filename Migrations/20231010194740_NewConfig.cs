using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnniesTech.Migrations
{
    /// <inheritdoc />
    public partial class NewConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Usuarios",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "ComentarioPadreId",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios",
                column: "ComentarioPadreId",
                principalTable: "Comentarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "Usuarios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComentarioPadreId",
                table: "Comentarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios",
                column: "ComentarioPadreId",
                principalTable: "Comentarios",
                principalColumn: "Id");
        }
    }
}
