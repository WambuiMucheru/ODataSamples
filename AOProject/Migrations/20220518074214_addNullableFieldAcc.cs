using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOProject.Migrations
{
    public partial class addNullableFieldAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceOfFunds",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceOfFunds",
                table: "Accounts");
        }
    }
}
