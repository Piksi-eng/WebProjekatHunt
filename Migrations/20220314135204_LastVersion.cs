using Microsoft.EntityFrameworkCore.Migrations;

namespace WebHunt.Migrations
{
    public partial class LastVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gun",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GunDmg = table.Column<int>(type: "int", nullable: false),
                    GunRange = table.Column<int>(type: "int", nullable: false),
                    GunSpeed = table.Column<int>(type: "int", nullable: false),
                    GunPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gun", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SecondGun",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SGunName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SGunDmg = table.Column<int>(type: "int", nullable: false),
                    SGunRange = table.Column<int>(type: "int", nullable: false),
                    SGunSpeed = table.Column<int>(type: "int", nullable: false),
                    SGunPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondGun", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToolDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToolPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Variant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DmgC = table.Column<int>(type: "int", nullable: false),
                    RangeC = table.Column<int>(type: "int", nullable: false),
                    SpeedC = table.Column<int>(type: "int", nullable: false),
                    PriceC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GunsVariant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunID = table.Column<int>(type: "int", nullable: false),
                    VariantID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GunsVariant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GunsVariant_Gun_GunID",
                        column: x => x.GunID,
                        principalTable: "Gun",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GunsVariant_Variant_VariantID",
                        column: x => x.VariantID,
                        principalTable: "Variant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loadout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LDmg = table.Column<int>(type: "int", nullable: false),
                    LRange = table.Column<int>(type: "int", nullable: false),
                    LSpeed = table.Column<int>(type: "int", nullable: false),
                    LPrice = table.Column<int>(type: "int", nullable: false),
                    SecondGunID = table.Column<int>(type: "int", nullable: true),
                    ToolID = table.Column<int>(type: "int", nullable: true),
                    GunsVariantID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loadout", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loadout_GunsVariant_GunsVariantID",
                        column: x => x.GunsVariantID,
                        principalTable: "GunsVariant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loadout_SecondGun_SecondGunID",
                        column: x => x.SecondGunID,
                        principalTable: "SecondGun",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loadout_Tools_ToolID",
                        column: x => x.ToolID,
                        principalTable: "Tools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GunsVariant_GunID",
                table: "GunsVariant",
                column: "GunID");

            migrationBuilder.CreateIndex(
                name: "IX_GunsVariant_VariantID",
                table: "GunsVariant",
                column: "VariantID");

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_GunsVariantID",
                table: "Loadout",
                column: "GunsVariantID");

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_SecondGunID",
                table: "Loadout",
                column: "SecondGunID");

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_ToolID",
                table: "Loadout",
                column: "ToolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loadout");

            migrationBuilder.DropTable(
                name: "GunsVariant");

            migrationBuilder.DropTable(
                name: "SecondGun");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "Gun");

            migrationBuilder.DropTable(
                name: "Variant");
        }
    }
}
