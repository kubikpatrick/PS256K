using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS256K.Migrations
{
    /// <inheritdoc />
    public partial class AddGeolocationPropertiesToPictureEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Pictures",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Pictures",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Pictures");
        }
    }
}
