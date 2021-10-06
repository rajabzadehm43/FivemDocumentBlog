using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddApiSetsAndCategoriesTblToNatives2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApiId",
                table: "NativeApiSets",
                newName: "ApiSetId");

            migrationBuilder.AddColumn<int>(
                name: "ApiSetId",
                table: "Natives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Natives",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ApiSetId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "NativeApiSetApiSetId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "NativeCategoryCategoryId",
                table: "Natives");

            migrationBuilder.RenameColumn(
                name: "ApiSetId",
                table: "NativeApiSets",
                newName: "ApiId");
        }
    }
}
