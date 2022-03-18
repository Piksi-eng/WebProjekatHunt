using Microsoft.EntityFrameworkCore.Migrations;

namespace WebHunt.Migrations
{
    public partial class addingTools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loadout_Tools_ToolID",
                table: "Loadout");

            migrationBuilder.RenameColumn(
                name: "ToolID",
                table: "Loadout",
                newName: "Tool4ID");

            migrationBuilder.RenameIndex(
                name: "IX_Loadout_ToolID",
                table: "Loadout",
                newName: "IX_Loadout_Tool4ID");

            migrationBuilder.AlterColumn<string>(
                name: "LName",
                table: "Loadout",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tool1ID",
                table: "Loadout",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tool2ID",
                table: "Loadout",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tool3ID",
                table: "Loadout",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_Tool1ID",
                table: "Loadout",
                column: "Tool1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_Tool2ID",
                table: "Loadout",
                column: "Tool2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Loadout_Tool3ID",
                table: "Loadout",
                column: "Tool3ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Loadout_Tools_Tool1ID",
                table: "Loadout",
                column: "Tool1ID",
                principalTable: "Tools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loadout_Tools_Tool2ID",
                table: "Loadout",
                column: "Tool2ID",
                principalTable: "Tools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loadout_Tools_Tool3ID",
                table: "Loadout",
                column: "Tool3ID",
                principalTable: "Tools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loadout_Tools_Tool4ID",
                table: "Loadout",
                column: "Tool4ID",
                principalTable: "Tools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loadout_Tools_Tool1ID",
                table: "Loadout");

            migrationBuilder.DropForeignKey(
                name: "FK_Loadout_Tools_Tool2ID",
                table: "Loadout");

            migrationBuilder.DropForeignKey(
                name: "FK_Loadout_Tools_Tool3ID",
                table: "Loadout");

            migrationBuilder.DropForeignKey(
                name: "FK_Loadout_Tools_Tool4ID",
                table: "Loadout");

            migrationBuilder.DropIndex(
                name: "IX_Loadout_Tool1ID",
                table: "Loadout");

            migrationBuilder.DropIndex(
                name: "IX_Loadout_Tool2ID",
                table: "Loadout");

            migrationBuilder.DropIndex(
                name: "IX_Loadout_Tool3ID",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "Tool1ID",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "Tool2ID",
                table: "Loadout");

            migrationBuilder.DropColumn(
                name: "Tool3ID",
                table: "Loadout");

            migrationBuilder.RenameColumn(
                name: "Tool4ID",
                table: "Loadout",
                newName: "ToolID");

            migrationBuilder.RenameIndex(
                name: "IX_Loadout_Tool4ID",
                table: "Loadout",
                newName: "IX_Loadout_ToolID");

            migrationBuilder.AlterColumn<string>(
                name: "LName",
                table: "Loadout",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Loadout_Tools_ToolID",
                table: "Loadout",
                column: "ToolID",
                principalTable: "Tools",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
