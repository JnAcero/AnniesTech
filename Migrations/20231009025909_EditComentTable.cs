using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnniesTech.Migrations
{
    /// <inheritdoc />
    public partial class EditComentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioPadreId",
                table: "Comentarios");

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
    }
}
