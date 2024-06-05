﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Game_Name",
                table: "Game",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_Name",
                table: "Character",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arcana_Name",
                table: "Arcana",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Game_Name",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Character_Name",
                table: "Character");

            migrationBuilder.DropIndex(
                name: "IX_Arcana_Name",
                table: "Arcana");
        }
    }
}
