using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Optional_Procedure_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_ProcedureCategories_ProcedureCategoryId",
                table: "Procedures");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureCategoryId",
                table: "Procedures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_ProcedureCategories_ProcedureCategoryId",
                table: "Procedures",
                column: "ProcedureCategoryId",
                principalTable: "ProcedureCategories",
                principalColumn: "ProcedureCategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_ProcedureCategories_ProcedureCategoryId",
                table: "Procedures");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureCategoryId",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_ProcedureCategories_ProcedureCategoryId",
                table: "Procedures",
                column: "ProcedureCategoryId",
                principalTable: "ProcedureCategories",
                principalColumn: "ProcedureCategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
