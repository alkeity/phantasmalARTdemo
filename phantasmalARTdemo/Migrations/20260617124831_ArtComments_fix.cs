using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhantasmalARTdemo.Migrations
{
    /// <inheritdoc />
    public partial class ArtComments_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtComment_Arts_ArtID",
                table: "ArtComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtComment_Users_UserID",
                table: "ArtComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtComment",
                table: "ArtComment");

            migrationBuilder.RenameTable(
                name: "ArtComment",
                newName: "ArtComments");

            migrationBuilder.RenameIndex(
                name: "IX_ArtComment_UserID",
                table: "ArtComments",
                newName: "IX_ArtComments_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ArtComment_ArtID",
                table: "ArtComments",
                newName: "IX_ArtComments_ArtID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ArtComments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ArtComments",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ArtComments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtComments",
                table: "ArtComments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtComments_Arts_ArtID",
                table: "ArtComments",
                column: "ArtID",
                principalTable: "Arts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtComments_Users_UserID",
                table: "ArtComments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtComments_Arts_ArtID",
                table: "ArtComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtComments_Users_UserID",
                table: "ArtComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtComments",
                table: "ArtComments");

            migrationBuilder.RenameTable(
                name: "ArtComments",
                newName: "ArtComment");

            migrationBuilder.RenameIndex(
                name: "IX_ArtComments_UserID",
                table: "ArtComment",
                newName: "IX_ArtComment_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ArtComments_ArtID",
                table: "ArtComment",
                newName: "IX_ArtComment_ArtID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ArtComment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ArtComment",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ArtComment",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtComment",
                table: "ArtComment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtComment_Arts_ArtID",
                table: "ArtComment",
                column: "ArtID",
                principalTable: "Arts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtComment_Users_UserID",
                table: "ArtComment",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
