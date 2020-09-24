using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTokenCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "TB_M_User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "TB_M_User");
        }
    }
}
