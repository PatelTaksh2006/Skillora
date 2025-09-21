using Microsoft.EntityFrameworkCore.Migrations;

namespace Skillora.Migrations
{
    public partial class DatabseOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobConstraints_Jobs_JobId",
                table: "JobConstraints");

            migrationBuilder.AddForeignKey(
                name: "FK_JobConstraints_Jobs_JobId",
                table: "JobConstraints",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobConstraints_Jobs_JobId",
                table: "JobConstraints");

            migrationBuilder.AddForeignKey(
                name: "FK_JobConstraints_Jobs_JobId",
                table: "JobConstraints",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
