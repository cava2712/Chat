using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Server.Migrations
{
    /// <inheritdoc />
    public partial class migrazioneMessageMAnager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messaggi_Utenti_UtenteUsername",
                table: "Messaggi");

            migrationBuilder.RenameColumn(
                name: "UtenteUsername",
                table: "Messaggi",
                newName: "MittenteUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Messaggi_UtenteUsername",
                table: "Messaggi",
                newName: "IX_Messaggi_MittenteUsername");

            migrationBuilder.AddColumn<string>(
                name: "DestinatarioUsername",
                table: "Messaggi",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_DestinatarioUsername",
                table: "Messaggi",
                column: "DestinatarioUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Messaggi_Utenti_DestinatarioUsername",
                table: "Messaggi",
                column: "DestinatarioUsername",
                principalTable: "Utenti",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Messaggi_Utenti_MittenteUsername",
                table: "Messaggi",
                column: "MittenteUsername",
                principalTable: "Utenti",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messaggi_Utenti_DestinatarioUsername",
                table: "Messaggi");

            migrationBuilder.DropForeignKey(
                name: "FK_Messaggi_Utenti_MittenteUsername",
                table: "Messaggi");

            migrationBuilder.DropIndex(
                name: "IX_Messaggi_DestinatarioUsername",
                table: "Messaggi");

            migrationBuilder.DropColumn(
                name: "DestinatarioUsername",
                table: "Messaggi");

            migrationBuilder.RenameColumn(
                name: "MittenteUsername",
                table: "Messaggi",
                newName: "UtenteUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Messaggi_MittenteUsername",
                table: "Messaggi",
                newName: "IX_Messaggi_UtenteUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Messaggi_Utenti_UtenteUsername",
                table: "Messaggi",
                column: "UtenteUsername",
                principalTable: "Utenti",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
