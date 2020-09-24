using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updColEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_User_EmpId",
                table: "TB_M_Employee");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "TB_M_Employee",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_User_UserId",
                table: "TB_M_Employee",
                column: "UserId",
                principalTable: "TB_M_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_User_UserId",
                table: "TB_M_Employee");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TB_M_Employee",
                newName: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_User_EmpId",
                table: "TB_M_Employee",
                column: "EmpId",
                principalTable: "TB_M_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
