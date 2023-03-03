using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Note");

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "Note",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_StatusType",
                table: "Note",
                column: "StatusType");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note",
                column: "StatusType",
                principalTable: "Status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Status_StatusType",
                table: "Note");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Note_StatusType",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "Note");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Note",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
