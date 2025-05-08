using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AM.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class mddede : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_MyPlane_planefk",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "planefk",
                table: "Flights",
                newName: "PlaneId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_planefk",
                table: "Flights",
                newName: "IX_Flights_PlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_MyPlane_PlaneId",
                table: "Flights",
                column: "PlaneId",
                principalTable: "MyPlane",
                principalColumn: "PlaneId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_MyPlane_PlaneId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "PlaneId",
                table: "Flights",
                newName: "planefk");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_PlaneId",
                table: "Flights",
                newName: "IX_Flights_planefk");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_MyPlane_planefk",
                table: "Flights",
                column: "planefk",
                principalTable: "MyPlane",
                principalColumn: "PlaneId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
