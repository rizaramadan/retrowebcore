using Microsoft.EntityFrameworkCore.Migrations;

namespace retrowebcore.Migrations
{
    public partial class boardidoncard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "board_id",
                table: "cards",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "board_id",
                table: "cards");
        }
    }
}
