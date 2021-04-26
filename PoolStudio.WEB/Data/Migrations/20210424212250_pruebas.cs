using Microsoft.EntityFrameworkCore.Migrations;

namespace PoolStudio.WEB.Data.Migrations
{
    public partial class pruebas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemTests",
                columns: table => new
                {
                    ItemTestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(maxLength: 100, nullable: true),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTests", x => x.ItemTestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemTests");
        }
    }
}
