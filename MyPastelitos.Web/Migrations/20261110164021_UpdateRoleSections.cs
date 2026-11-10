using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPastelitos.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleSections",
                columns: table => new
                {
                    PrivateRecipeRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSections", x => new { x.PrivateRecipeRoleId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_RoleSections_PrivateRecipeRoles_PrivateRecipeRoleId",
                        column: x => x.PrivateRecipeRoleId,
                        principalTable: "PrivateRecipeRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSections_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleSections_SectionId",
                table: "RoleSections",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleSections");
        }
    }
}
