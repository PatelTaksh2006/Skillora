using Microsoft.EntityFrameworkCore.Migrations;

namespace Skillora.Migrations
{
    public partial class SelectedStudentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SelectedStudentJob",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SelectedStudentJob");
        }
    }
}
