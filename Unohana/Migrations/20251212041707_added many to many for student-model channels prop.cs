using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unohana.Migrations
{
    /// <inheritdoc />
    public partial class addedmanytomanyforstudentmodelchannelsprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Channels_ChannelId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ChannelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "ChannelStudent",
                columns: table => new
                {
                    ChannelsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelStudent", x => new { x.ChannelsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ChannelStudent_Channels_ChannelsId",
                        column: x => x.ChannelsId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStudent_StudentsId",
                table: "ChannelStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelStudent");

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ChannelId",
                table: "Students",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Channels_ChannelId",
                table: "Students",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");
        }
    }
}
