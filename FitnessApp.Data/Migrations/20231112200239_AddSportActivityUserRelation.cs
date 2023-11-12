using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSportActivityUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportActivities_Users_UserId",
                table: "SportActivities");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SportActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SportActivities_Users_UserId",
                table: "SportActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportActivities_Users_UserId",
                table: "SportActivities");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SportActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SportActivities_Users_UserId",
                table: "SportActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
