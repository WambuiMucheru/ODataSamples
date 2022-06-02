using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOProject.Migrations
{
    public partial class rated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Branch_BranchId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Branch_BranchId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Branches_BranchId",
                table: "Accounts",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Branches_BranchId",
                table: "Rating",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Branches_BranchId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Branches_BranchId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branch");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Branch_BranchId",
                table: "Accounts",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Branch_BranchId",
                table: "Rating",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
