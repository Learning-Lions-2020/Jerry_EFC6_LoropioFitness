using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class sensordatacreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportEventUser");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SportEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "SportEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "SportActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "SportActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SensorDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AverageHeartRate = table.Column<double>(type: "float", nullable: false),
                    CalorieConsumption = table.Column<double>(type: "float", nullable: false),
                    AverageBodyTemperature = table.Column<double>(type: "float", nullable: false),
                    SportActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorDatas_SportActivities_SportActivityId",
                        column: x => x.SportActivityId,
                        principalTable: "SportActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSportEvents",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SportEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSportEvents", x => new { x.UserId, x.SportEventId });
                    table.ForeignKey(
                        name: "FK_UserSportEvents_SportEvents_SportEventId",
                        column: x => x.SportEventId,
                        principalTable: "SportEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSportEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportEvents_UserId",
                table: "SportEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SportEvents_UserId1",
                table: "SportEvents",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_SensorDatas_SportActivityId",
                table: "SensorDatas",
                column: "SportActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSportEvents_SportEventId",
                table: "UserSportEvents",
                column: "SportEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_Users_UserId",
                table: "SportEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SportEvents_Users_UserId1",
                table: "SportEvents",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_Users_UserId",
                table: "SportEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_SportEvents_Users_UserId1",
                table: "SportEvents");

            migrationBuilder.DropTable(
                name: "SensorDatas");

            migrationBuilder.DropTable(
                name: "UserSportEvents");

            migrationBuilder.DropIndex(
                name: "IX_SportEvents_UserId",
                table: "SportEvents");

            migrationBuilder.DropIndex(
                name: "IX_SportEvents_UserId1",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "SportEvents");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "SportActivities");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SportActivities");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "UserName");

            migrationBuilder.CreateTable(
                name: "SportEventUser",
                columns: table => new
                {
                    SportEventId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportEventUser", x => new { x.SportEventId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SportEventUser_SportEvents_SportEventId",
                        column: x => x.SportEventId,
                        principalTable: "SportEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportEventUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportEventUser_UsersId",
                table: "SportEventUser",
                column: "UsersId");
        }
    }
}
