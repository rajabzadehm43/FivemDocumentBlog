using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddAuthorToNativeTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Natives",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Natives_AuthorId",
                table: "Natives",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Natives_AspNetUsers_AuthorId",
                table: "Natives",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Natives_AspNetUsers_AuthorId",
                table: "Natives");

            migrationBuilder.DropIndex(
                name: "IX_Natives_AuthorId",
                table: "Natives");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Natives");
        }
    }
}
