using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS256K.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Customers");
        }
    }
}
