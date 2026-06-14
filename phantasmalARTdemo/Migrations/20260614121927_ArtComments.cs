using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phantasmalARTdemo.Migrations
{
    /// <inheritdoc />
    public partial class ArtComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtComment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtComment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtComment_Arts_ArtID",
                        column: x => x.ArtID,
                        principalTable: "Arts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtComment_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtComment_ArtID",
                table: "ArtComment",
                column: "ArtID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtComment_UserID",
                table: "ArtComment",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtComment");
        }
    }
}
