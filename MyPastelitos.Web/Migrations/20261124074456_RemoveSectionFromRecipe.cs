using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPastelitos.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSectionFromRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Sections_SectionId",
                table: "Recipe");

            migrationBuilder.DropIndex(
                name: "IX_Recipe_SectionId",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Recipe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SectionId",
                table: "Recipe",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_SectionId",
                table: "Recipe",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Sections_SectionId",
                table: "Recipe",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
