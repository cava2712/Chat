using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Server.Migrations
{
    /// <inheritdoc />
    public partial class fatture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fatture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MittenteUsername = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinatarioUsername = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrezzoNoIva = table.Column<float>(type: "real", nullable: false),
                    Iva = table.Column<int>(type: "int", nullable: false),
                    PrezzoFinale = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatture_Utenti_DestinatarioUsername",
                        column: x => x.DestinatarioUsername,
                        principalTable: "Utenti",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_Fatture_Utenti_MittenteUsername",
                        column: x => x.MittenteUsername,
                        principalTable: "Utenti",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fatture_DestinatarioUsername",
                table: "Fatture",
                column: "DestinatarioUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Fatture_MittenteUsername",
                table: "Fatture",
                column: "MittenteUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fatture");
        }
    }
}
