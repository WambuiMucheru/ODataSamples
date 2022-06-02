using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOProject.Migrations
{
    public partial class addintacc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "balance",
                table: "Accounts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "Accounts");
        }
    }
}
