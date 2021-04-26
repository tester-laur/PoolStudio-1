using Microsoft.EntityFrameworkCore.Migrations;

namespace PoolStudio.WEB.Data.Migrations
{
    public partial class fotos_de_productos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Item",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Item");
        }
    }
}
