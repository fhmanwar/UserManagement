using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addColProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "TB_M_Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "TB_M_Employee");
        }
    }
}
