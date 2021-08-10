using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedGamesAndSteps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                "GameId",
                "AspNetUsers",
                "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                "Games",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    IsFinished = table.Column<bool>("bit", nullable: false),
                    GuessedNumber = table.Column<int>("int", nullable: false),
                    WinnerId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    HostId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    EndTime = table.Column<DateTimeOffset>("datetimeoffset", nullable: false),
                    StartTime = table.Column<DateTimeOffset>("datetimeoffset", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Games", x => x.Id); });

            migrationBuilder.CreateTable(
                "Steps",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Time = table.Column<DateTimeOffset>("datetimeoffset", nullable: false),
                    Value = table.Column<int>("int", nullable: false),
                    StepNumber = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        "FK_Steps_Games_GameId",
                        x => x.GameId,
                        "Games",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_GameId",
                "AspNetUsers",
                "GameId");

            migrationBuilder.CreateIndex(
                "IX_Steps_GameId",
                "Steps",
                "GameId");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_Games_GameId",
                "AspNetUsers",
                "GameId",
                "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_Games_GameId",
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Steps");

            migrationBuilder.DropTable(
                "Games");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_GameId",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "GameId",
                "AspNetUsers");
        }
    }
}