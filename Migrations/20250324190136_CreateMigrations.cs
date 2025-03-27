using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS256K.Migrations
{
    /// <inheritdoc />
    public partial class CreateMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Albums_AlbumId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_AlbumId",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "Pictures",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProjectId",
                table: "Pictures",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Projects_ProjectId",
                table: "Pictures",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Projects_ProjectId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProjectId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Pictures");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AlbumId",
                table: "Pictures",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Albums_AlbumId",
                table: "Pictures",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
