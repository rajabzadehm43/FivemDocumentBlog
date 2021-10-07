using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddNativeTagsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NativeTags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NativeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NativeTags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_NativeTags_Natives_NativeId",
                        column: x => x.NativeId,
                        principalTable: "Natives",
                        principalColumn: "NativeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NativeTags_NativeId",
                table: "NativeTags",
                column: "NativeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NativeTags");
        }
    }
}
