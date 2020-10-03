using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class dropTblApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Apps_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Apps");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "TB_M_Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppId",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_Apps",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    DeleteDate = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: false),
                    isDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Apps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_AppId",
                table: "TB_M_Employee",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Apps_AppId",
                table: "TB_M_Employee",
                column: "AppId",
                principalTable: "TB_M_Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
