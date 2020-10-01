using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DivisionId",
                table: "TB_M_Employee");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "TB_M_Employee",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_M_Employee_DivisionId",
                table: "TB_M_Employee",
                newName: "IX_TB_M_Employee_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DepartmentId",
                table: "TB_M_Employee",
                column: "DepartmentId",
                principalTable: "TB_M_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DepartmentId",
                table: "TB_M_Employee");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "TB_M_Employee",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_M_Employee_DepartmentId",
                table: "TB_M_Employee",
                newName: "IX_TB_M_Employee_DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_M_Employee_TB_M_Department_DivisionId",
                table: "TB_M_Employee",
                column: "DivisionId",
                principalTable: "TB_M_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
