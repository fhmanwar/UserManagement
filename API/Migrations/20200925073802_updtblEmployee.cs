using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updtblEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrict",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Village",
                table: "TB_M_Employee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "TB_M_Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "SubDistrict",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "Village",
                table: "TB_M_Employee");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "TB_M_Employee");
        }
    }
}
