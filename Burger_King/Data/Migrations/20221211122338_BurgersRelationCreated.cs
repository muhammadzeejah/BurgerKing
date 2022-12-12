using Microsoft.EntityFrameworkCore.Migrations;

namespace Burger_King.Data.Migrations
{
    public partial class BurgersRelationCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    burger_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    burger_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    burger_price = table.Column<float>(type: "real", nullable: false),
                    burger_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sauce_fid = table.Column<int>(type: "int", nullable: false),
                    addon_fid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.burger_id);
                    table.ForeignKey(
                        name: "FK_Burgers_Add_on_addon_fid",
                        column: x => x.addon_fid,
                        principalTable: "Add_on",
                        principalColumn: "addon_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Burgers_Sauces_sauce_fid",
                        column: x => x.sauce_fid,
                        principalTable: "Sauces",
                        principalColumn: "sauce_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Burgers_addon_fid",
                table: "Burgers",
                column: "addon_fid");

            migrationBuilder.CreateIndex(
                name: "IX_Burgers_sauce_fid",
                table: "Burgers",
                column: "sauce_fid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Burgers");
        }
    }
}
