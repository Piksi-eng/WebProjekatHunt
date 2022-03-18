using Microsoft.EntityFrameworkCore.Migrations;

namespace WebHunt.Migrations
{
    public partial class removedLoadoutInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LDmg",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "LPrice",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "LRange",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "LSpeed",
                table: "Loadout");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LDmg",
                table: "Loadout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LPrice",
                table: "Loadout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LRange",
                table: "Loadout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LSpeed",
                table: "Loadout",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
