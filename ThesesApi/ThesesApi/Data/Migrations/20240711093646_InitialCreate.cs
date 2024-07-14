using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesesApi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: false),
                    Workplace = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thesis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MainAuthorId = table.Column<long>(type: "INTEGER", nullable: false),
                    ContactEmail = table.Column<string>(type: "TEXT", nullable: true),
                    Topic = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thesis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thesis_persons_MainAuthorId",
                        column: x => x.MainAuthorId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "otherAuthors",
                columns: table => new
                {
                    ThesisId = table.Column<long>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_otherAuthors", x => new { x.ThesisId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_otherAuthors_persons_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_otherAuthors_thesis_ThesisId",
                        column: x => x.ThesisId,
                        principalTable: "thesis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_otherAuthors_AuthorId",
                table: "otherAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_thesis_MainAuthorId",
                table: "thesis",
                column: "MainAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "otherAuthors");

            migrationBuilder.DropTable(
                name: "thesis");

            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
