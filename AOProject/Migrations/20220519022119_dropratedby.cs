using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOProject.Migrations
{
    public partial class dropratedby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Customers_RatedById",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RatedById",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "RatedById",
                table: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatedById",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatedById",
                table: "Rating",
                column: "RatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Customers_RatedById",
                table: "Rating",
                column: "RatedById",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
