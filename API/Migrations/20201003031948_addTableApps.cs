using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTableApps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Absent_TB_M_Employee_UserId",
                table: "TB_M_Absent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Absent",
                table: "TB_M_Absent");

            migrationBuilder.RenameTable(
                name: "TB_M_Absent",
                newName: "TB_T_Absent");

            migrationBuilder.RenameIndex(
                name: "IX_TB_M_Absent_UserId",
                table: "TB_T_Absent",
                newName: "IX_TB_T_Absent_UserId");

            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_T_Absent",
                table: "TB_T_Absent",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TB_M_Apps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: false),
                    DeleteDate = table.Column<DateTimeOffset>(nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Absent_TB_M_Employee_UserId",
                table: "TB_T_Absent",
                column: "UserId",
                principalTable: "TB_M_Employee",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Apps_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Absent_TB_M_Employee_UserId",
                table: "TB_T_Absent");

            migrationBuilder.DropTable(
                name: "TB_M_Apps");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_T_Absent",
                table: "TB_T_Absent");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "TB_M_Employee");

            migrationBuilder.RenameTable(
                name: "TB_T_Absent",
                newName: "TB_M_Absent");

            migrationBuilder.RenameIndex(
                name: "IX_TB_T_Absent_UserId",
                table: "TB_M_Absent",
                newName: "IX_TB_M_Absent_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Absent",
                table: "TB_M_Absent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Absent_TB_M_Employee_UserId",
                table: "TB_M_Absent",
                column: "UserId",
                principalTable: "TB_M_Employee",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
