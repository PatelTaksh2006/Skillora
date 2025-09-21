using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Skillora.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Students",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CompanyViewModelId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Industry = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeleteJobViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    companyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteJobViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyViewModelId",
                table: "Jobs",
                column: "CompanyViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_CompanyViewModel_CompanyViewModelId",
                table: "Jobs",
                column: "CompanyViewModelId",
                principalTable: "CompanyViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_CompanyViewModel_CompanyViewModelId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "CompanyViewModel");

            migrationBuilder.DropTable(
                name: "DeleteJobViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyViewModelId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyViewModelId",
                table: "Jobs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "Students",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Companies_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
