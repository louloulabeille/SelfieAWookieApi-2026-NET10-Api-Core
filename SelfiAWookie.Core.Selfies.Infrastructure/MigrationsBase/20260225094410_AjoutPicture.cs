using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SelfieAWookie.Core.Selfies.Infrastructure.MigrationsBase
{
    /// <inheritdoc />
    public partial class AjoutPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PictureId",
                table: "Selfie",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Selfie_PictureId",
                table: "Selfie",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Selfie_Pictures_PictureId",
                table: "Selfie",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selfie_Pictures_PictureId",
                table: "Selfie");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Selfie_PictureId",
                table: "Selfie");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Selfie");
        }
    }
}
