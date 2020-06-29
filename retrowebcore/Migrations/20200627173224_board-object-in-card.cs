using Microsoft.EntityFrameworkCore.Migrations;

namespace retrowebcore.Migrations
{
    public partial class boardobjectincard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_cards_board_id",
                table: "cards",
                column: "board_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cards_boards_board_id",
                table: "cards",
                column: "board_id",
                principalTable: "boards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cards_boards_board_id",
                table: "cards");

            migrationBuilder.DropIndex(
                name: "ix_cards_board_id",
                table: "cards");
        }
    }
}
