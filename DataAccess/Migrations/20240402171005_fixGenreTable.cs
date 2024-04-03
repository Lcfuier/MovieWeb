using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixGenreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Genre_GenreId",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_GenreId",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Rating",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_GenreId",
                table: "Rating",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Genre_GenreId",
                table: "Rating",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId");
        }
    }
}
