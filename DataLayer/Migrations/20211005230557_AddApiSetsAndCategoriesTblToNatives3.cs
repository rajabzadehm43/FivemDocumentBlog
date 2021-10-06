using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddApiSetsAndCategoriesTblToNatives3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Natives_NativeApiSets_NativeApiSetApiSetId",
                table: "Natives");

            migrationBuilder.DropForeignKey(
                name: "FK_Natives_NativeCategories_NativeCategoryCategoryId",
                table: "Natives");

            migrationBuilder.DropIndex(
                name: "IX_Natives_NativeApiSetApiSetId",
                table: "Natives");

            migrationBuilder.DropIndex(
                name: "IX_Natives_NativeCategoryCategoryId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "NativeApiSetApiSetId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "NativeCategoryCategoryId",
                table: "Natives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NativeApiSetApiSetId",
                table: "Natives",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NativeCategoryCategoryId",
                table: "Natives",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Natives_NativeApiSetApiSetId",
                table: "Natives",
                column: "NativeApiSetApiSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Natives_NativeCategoryCategoryId",
                table: "Natives",
                column: "NativeCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Natives_NativeApiSets_NativeApiSetApiSetId",
                table: "Natives",
                column: "NativeApiSetApiSetId",
                principalTable: "NativeApiSets",
                principalColumn: "ApiSetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Natives_NativeCategories_NativeCategoryCategoryId",
                table: "Natives",
                column: "NativeCategoryCategoryId",
                principalTable: "NativeCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
