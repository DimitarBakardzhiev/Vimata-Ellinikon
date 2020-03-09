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

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Type" },
                values: new object[] { 1, new DateTime(2020, 3, 9, 16, 55, 5, 163, DateTimeKind.Local).AddTicks(2358), null, "Gold" });

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Type" },
                values: new object[] { 2, new DateTime(2020, 3, 9, 16, 55, 5, 165, DateTimeKind.Local).AddTicks(1508), null, "Silver" });

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Type" },
                values: new object[] { 3, new DateTime(2020, 3, 9, 16, 55, 5, 165, DateTimeKind.Local).AddTicks(1547), null, "Bronze" });

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

            migrationBuilder.DeleteData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
