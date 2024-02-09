using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turnir.DAL.Migrations
{
    public partial class AddTwoRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TownTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountWin = table.Column<int>(type: "int", nullable: false),
                    CountDefeats = table.Column<int>(type: "int", nullable: false),
                    CountGames = table.Column<int>(type: "int", nullable: false),
                    CountGoalSC = table.Column<int>(type: "int", nullable: false),
                    CountGoalCo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Club");
        }
    }
}
