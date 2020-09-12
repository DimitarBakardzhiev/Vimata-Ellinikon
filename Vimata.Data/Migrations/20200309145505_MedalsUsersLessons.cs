using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class MedalsUsersLessons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedalsUsersLessons",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MedalId = table.Column<int>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedalsUsersLessons", x => new { x.MedalId, x.UserId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_MedalsUsersLessons_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedalsUsersLessons_Medals_MedalId",
                        column: x => x.MedalId,
                        principalTable: "Medals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedalsUsersLessons_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedalsUsersLessons_LessonId",
                table: "MedalsUsersLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_MedalsUsersLessons_UserId",
                table: "MedalsUsersLessons",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedalsUsersLessons");
        }
    }
}
