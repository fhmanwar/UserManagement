using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updColumnTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteData",
                table: "TB_M_Role",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "TB_M_Role",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "DeleteData",
                table: "TB_M_Employee",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "TB_M_Employee",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "TB_M_Role",
                newName: "DeleteData");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "TB_M_Role",
                newName: "CreateData");

            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "TB_M_Employee",
                newName: "DeleteData");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "TB_M_Employee",
                newName: "CreateData");
        }
    }
}
