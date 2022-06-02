using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOProject.Migrations
{
    public partial class changeaddaccfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "balance",
                table: "Accounts",
                newName: "WorkingBalance");

            migrationBuilder.AddColumn<int>(
                name: "OnlineBalance",
                table: "Accounts",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineBalance",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "WorkingBalance",
                table: "Accounts",
                newName: "balance");
        }
    }
}
