using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unohana.Migrations
{
    /// <inheritdoc />
    public partial class addedemployeeIdpropintutors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Tutors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Tutors");
        }
    }
}
