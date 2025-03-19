using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS256K.Migrations
{
    /// <inheritdoc />
    public partial class RenameHashPropertyToPathOfAlbumEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "Medias",
                newName: "Path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Medias",
                newName: "Hash");
        }
    }
}
