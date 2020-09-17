using Microsoft.EntityFrameworkCore.Migrations;

namespace Image_Gallery.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    CategoryMediaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryViewModel_Media_CategoryMediaId",
                        column: x => x.CategoryMediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreateCategoryViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateCategoryViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditCategoryViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditCategoryViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(nullable: true),
                    CategoryTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaViewModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryViewModel_CategoryMediaId",
                table: "CategoryViewModel",
                column: "CategoryMediaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryViewModel");

            migrationBuilder.DropTable(
                name: "CreateCategoryViewModel");

            migrationBuilder.DropTable(
                name: "EditCategoryViewModel");

            migrationBuilder.DropTable(
                name: "MediaViewModel");
        }
    }
}
