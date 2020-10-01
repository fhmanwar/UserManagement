using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updNamingDivisionToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Division_DivisionId",
                table: "TB_M_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Division",
                table: "TB_M_Division");

            migrationBuilder.RenameTable(
                name: "TB_M_Division",
                newName: "TB_M_Department");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Department",
                table: "TB_M_Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DivisionId",
                table: "TB_M_Employee",
                column: "DivisionId",
                principalTable: "TB_M_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DivisionId",
                table: "TB_M_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_Department",
                table: "TB_M_Department");

            migrationBuilder.RenameTable(
                name: "TB_M_Department",
                newName: "TB_M_Division");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_Division",
                table: "TB_M_Division",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Division_DivisionId",
                table: "TB_M_Employee",
                column: "DivisionId",
                principalTable: "TB_M_Division",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
