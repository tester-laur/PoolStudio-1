using Microsoft.EntityFrameworkCore.Migrations;

namespace PoolStudio.WEB.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clasification",
                columns: table => new
                {
                    ClasificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemType = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clasification", x => x.ClasificationId);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasificationId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(maxLength: 100, nullable: true),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Clasification_ClasificationId",
                        column: x => x.ClasificationId,
                        principalTable: "Clasification",
                        principalColumn: "ClasificationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ClasificationId",
                table: "Item",
                column: "ClasificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Clasification");
        }
    }
}
