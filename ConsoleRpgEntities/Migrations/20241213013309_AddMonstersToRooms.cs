using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    /// <inheritdoc />
    public partial class AddMonstersToRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Monsters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_RoomId",
                table: "Monsters",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Rooms_RoomId",
                table: "Monsters",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Rooms_RoomId",
                table: "Monsters");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_RoomId",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Monsters");
        }
    }
}
