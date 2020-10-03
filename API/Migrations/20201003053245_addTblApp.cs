using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTblApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TB_M_App",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdateDate = table.Column<DateTimeOffset>(nullable: false),
                    DeleteDate = table.Column<DateTimeOffset>(nullable: false),
                    isDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_App", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_AppId",
                table: "TB_M_Employee",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_App_AppId",
                table: "TB_M_Employee",
                column: "AppId",
                principalTable: "TB_M_App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_App_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_App");

            migrationBuilder.DropIndex(
                name: "IX_TB_M_Employee_AppId",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "TB_M_Employee");
        }
    }
}
