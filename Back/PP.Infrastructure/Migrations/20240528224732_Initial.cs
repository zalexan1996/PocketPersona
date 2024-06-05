using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arcana",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arcana", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArcanaGame",
                columns: table => new
                {
                    AppearsInId = table.Column<int>(type: "int", nullable: false),
                    ArcanaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArcanaGame", x => new { x.AppearsInId, x.ArcanaId });
                    table.ForeignKey(
                        name: "FK_ArcanaGame_Arcana_ArcanaId",
                        column: x => x.ArcanaId,
                        principalTable: "Arcana",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArcanaGame_Game_AppearsInId",
                        column: x => x.AppearsInId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AppearsInId = table.Column<int>(type: "int", nullable: false),
                    Gifts = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Game_AppearsInId",
                        column: x => x.AppearsInId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UnlockConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    ArcanaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialLink_Arcana_ArcanaId",
                        column: x => x.ArcanaId,
                        principalTable: "Arcana",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocialLink_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialLinkDialogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SocialLinkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLinkDialogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialLinkDialogue_SocialLink_SocialLinkId",
                        column: x => x.SocialLinkId,
                        principalTable: "SocialLink",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArcanaGame_ArcanaId",
                table: "ArcanaGame",
                column: "ArcanaId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_AppearsInId",
                table: "Character",
                column: "AppearsInId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_ArcanaId",
                table: "SocialLink",
                column: "ArcanaId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialLink_CharacterId",
                table: "SocialLink",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialLinkDialogue_SocialLinkId",
                table: "SocialLinkDialogue",
                column: "SocialLinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArcanaGame");

            migrationBuilder.DropTable(
                name: "SocialLinkDialogue");

            migrationBuilder.DropTable(
                name: "SocialLink");

            migrationBuilder.DropTable(
                name: "Arcana");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
