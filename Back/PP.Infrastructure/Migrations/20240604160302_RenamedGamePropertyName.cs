using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedGamePropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArcanaGame_Arcana_ArcanaId",
                table: "ArcanaGame");

            migrationBuilder.DropForeignKey(
                name: "FK_ArcanaGame_Game_AppearsInId",
                table: "ArcanaGame");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Game_AppearsInId",
                table: "Character");

            migrationBuilder.RenameColumn(
                name: "AppearsInId",
                table: "Character",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Character_AppearsInId",
                table: "Character",
                newName: "IX_Character_GameId");

            migrationBuilder.RenameColumn(
                name: "ArcanaId",
                table: "ArcanaGame",
                newName: "GamesId");

            migrationBuilder.RenameColumn(
                name: "AppearsInId",
                table: "ArcanaGame",
                newName: "ArcanasId");

            migrationBuilder.RenameIndex(
                name: "IX_ArcanaGame_ArcanaId",
                table: "ArcanaGame",
                newName: "IX_ArcanaGame_GamesId");

            migrationBuilder.AlterColumn<string>(
                name: "Gifts",
                table: "Character",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ArcanaGame_Arcana_ArcanasId",
                table: "ArcanaGame",
                column: "ArcanasId",
                principalTable: "Arcana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArcanaGame_Game_GamesId",
                table: "ArcanaGame",
                column: "GamesId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Game_GameId",
                table: "Character",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArcanaGame_Arcana_ArcanasId",
                table: "ArcanaGame");

            migrationBuilder.DropForeignKey(
                name: "FK_ArcanaGame_Game_GamesId",
                table: "ArcanaGame");

            migrationBuilder.DropForeignKey(
                name: "FK_Character_Game_GameId",
                table: "Character");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Character",
                newName: "AppearsInId");

            migrationBuilder.RenameIndex(
                name: "IX_Character_GameId",
                table: "Character",
                newName: "IX_Character_AppearsInId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "ArcanaGame",
                newName: "ArcanaId");

            migrationBuilder.RenameColumn(
                name: "ArcanasId",
                table: "ArcanaGame",
                newName: "AppearsInId");

            migrationBuilder.RenameIndex(
                name: "IX_ArcanaGame_GamesId",
                table: "ArcanaGame",
                newName: "IX_ArcanaGame_ArcanaId");

            migrationBuilder.AlterColumn<string>(
                name: "Gifts",
                table: "Character",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ArcanaGame_Arcana_ArcanaId",
                table: "ArcanaGame",
                column: "ArcanaId",
                principalTable: "Arcana",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArcanaGame_Game_AppearsInId",
                table: "ArcanaGame",
                column: "AppearsInId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Game_AppearsInId",
                table: "Character",
                column: "AppearsInId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
