using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMyPG.Data.Migrations
{
    public partial class PGInfoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PGType",
                table: "PGInfo",
                newName: "PGCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PGCategory",
                table: "PGInfo",
                newName: "PGType");
        }
    }
}
