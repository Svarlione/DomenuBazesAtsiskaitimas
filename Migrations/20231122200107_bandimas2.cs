using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomenuBazesAtsiskaitimas.Migrations
{
    /// <inheritdoc />
    public partial class bandimas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnicCode",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnicCode",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnicCode",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnicCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UnicCode",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "UnicCode",
                table: "Faculties");
        }
    }
}
