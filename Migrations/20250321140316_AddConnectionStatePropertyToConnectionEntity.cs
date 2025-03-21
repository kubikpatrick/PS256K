using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS256K.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionStatePropertyToConnectionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Connections",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Connections");
        }
    }
}
