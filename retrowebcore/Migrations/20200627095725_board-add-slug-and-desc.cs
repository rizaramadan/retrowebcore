using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace retrowebcore.Migrations
{
    public partial class boardaddsluganddesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "boards",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "slug",
                table: "boards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_boards_slug",
                table: "boards",
                column: "slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_boards_slug",
                table: "boards");

            migrationBuilder.DropColumn(
                name: "description",
                table: "boards");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "boards");
        }
    }
}
