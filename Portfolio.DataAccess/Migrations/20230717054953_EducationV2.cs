using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EducationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfPassing",
                table: "Educations");

            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Session",
                table: "Educations");

            migrationBuilder.AddColumn<int>(
                name: "YearOfPassing",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
