using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkToDo.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItem_AspNetUsers_UserId1",
                table: "WorkItem");

            migrationBuilder.DropIndex(
                name: "IX_WorkItem_UserId1",
                table: "WorkItem");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "WorkItem");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WorkItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItem_UserId",
                table: "WorkItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItem_AspNetUsers_UserId",
                table: "WorkItem",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItem_AspNetUsers_UserId",
                table: "WorkItem");

            migrationBuilder.DropIndex(
                name: "IX_WorkItem_UserId",
                table: "WorkItem");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WorkItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "WorkItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItem_UserId1",
                table: "WorkItem",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItem_AspNetUsers_UserId1",
                table: "WorkItem",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
