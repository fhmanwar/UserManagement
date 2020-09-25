using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTblDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DivisionId",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_Division",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreateData = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: false),
                    DeleteData = table.Column<DateTimeOffset>(nullable: false),
                    isDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Division", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_DivisionId",
                table: "TB_M_Employee",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Division_DivisionId",
                table: "TB_M_Employee",
                column: "DivisionId",
                principalTable: "TB_M_Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Division_DivisionId",
                table: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Division");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_DivisionId",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "TB_M_Employee");
        }
    }
}
