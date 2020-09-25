using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updTblDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteData",
                table: "TB_M_Division",
                newName: "DeleteDate");

            migrationBuilder.RenameColumn(
                name: "CreateData",
                table: "TB_M_Division",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeleteDate",
                table: "TB_M_Division",
                newName: "DeleteData");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "TB_M_Division",
                newName: "CreateData");
        }
    }
}
