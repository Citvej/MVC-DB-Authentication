using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rain33.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uporabnik",
                columns: table => new
                {
                    UporabnikiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    Roles = table.Column<bool>(nullable: false),
                    salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uporabnik", x => x.UporabnikiId);
                });

            migrationBuilder.CreateTable(
                name: "Novica",
                columns: table => new
                {
                    NoviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    datum = table.Column<DateTime>(nullable: false),
                    avtor = table.Column<string>(nullable: true),
                    besedilo = table.Column<string>(nullable: true),
                    UporabnikiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Novica", x => x.NoviceId);
                    table.ForeignKey(
                        name: "FK_Novica_Uporabnik_UporabnikiId",
                        column: x => x.UporabnikiId,
                        principalTable: "Uporabnik",
                        principalColumn: "UporabnikiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Novica_UporabnikiId",
                table: "Novica",
                column: "UporabnikiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Novica");

            migrationBuilder.DropTable(
                name: "Uporabnik");
        }
    }
}
