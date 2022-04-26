using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mongemini.Service.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blanks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    SomeText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blanks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blanks");
        }
    }
}
