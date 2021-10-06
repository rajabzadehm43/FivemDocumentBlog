using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddApiSetsAndCategoriesTblToNatives4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Natives_ApiSetId",
                table: "Natives",
                column: "ApiSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Natives_CategoryId",
                table: "Natives",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Natives_NativeApiSets_ApiSetId",
                table: "Natives",
                column: "ApiSetId",
                principalTable: "NativeApiSets",
                principalColumn: "ApiSetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Natives_NativeCategories_CategoryId",
                table: "Natives",
                column: "CategoryId",
                principalTable: "NativeCategories",
                principalColumn: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Natives_NativeApiSets_ApiSetId",
                table: "Natives");

            migrationBuilder.DropForeignKey(
                name: "FK_Natives_NativeCategories_CategoryId",
                table: "Natives");

            migrationBuilder.DropIndex(
                name: "IX_Natives_ApiSetId",
                table: "Natives");

            migrationBuilder.DropIndex(
                name: "IX_Natives_CategoryId",
                table: "Natives");
        }
    }
}
