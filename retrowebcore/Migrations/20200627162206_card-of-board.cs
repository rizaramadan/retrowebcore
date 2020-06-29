using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace retrowebcore.Migrations
{
    public partial class cardofboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    slug = table.Column<Guid>(nullable: true),
                    card_type = table.Column<int>(nullable: false),
                    liker_id = table.Column<List<long>>(nullable: true),
                    related_card_id = table.Column<List<long>>(nullable: true),
                    creator = table.Column<long>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    updator = table.Column<long>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    deletor = table.Column<long>(nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    slug = table.Column<Guid>(nullable: true),
                    card_id = table.Column<long>(nullable: false),
                    creator = table.Column<long>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    updator = table.Column<long>(nullable: false),
                    updated = table.Column<DateTime>(nullable: false),
                    deletor = table.Column<long>(nullable: true),
                    deleted_at = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_comments_card_id",
                table: "comments",
                column: "card_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "cards");
        }
    }
}
