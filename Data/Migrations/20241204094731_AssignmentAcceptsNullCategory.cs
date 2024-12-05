using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkToDo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignmentAcceptsNullCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Category_CategoryId",
                table: "Assignment");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Assignment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Category_CategoryId",
                table: "Assignment",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_Category_CategoryId",
                table: "Assignment");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Assignment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_Category_CategoryId",
                table: "Assignment",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
