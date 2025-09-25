using Microsoft.EntityFrameworkCore.Migrations;

namespace Skillora.Migrations
{
    public partial class StudentJobChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "applied",
                table: "StudentJob",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applied",
                table: "StudentJob");
        }
    }
}
