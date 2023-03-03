using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class FixNoteStatusFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "StatusType",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note",
                column: "StatusType",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "StatusType",
                table: "Note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note",
                column: "StatusType",
                principalTable: "Status",
                principalColumn: "Id");
        }
    }
}
