using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class MedalsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Medals",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Medals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Medals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medals_UserId",
                table: "Medals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medals_LessonId_UserId_Type",
                table: "Medals",
                columns: new[] { "LessonId", "UserId", "Type" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medals_Lessons_LessonId",
                table: "Medals",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medals_Users_UserId",
                table: "Medals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medals_Lessons_LessonId",
                table: "Medals");

            migrationBuilder.DropForeignKey(
                name: "FK_Medals_Users_UserId",
                table: "Medals");

            migrationBuilder.DropIndex(
                name: "IX_Medals_UserId",
                table: "Medals");

            migrationBuilder.DropIndex(
                name: "IX_Medals_LessonId_UserId_Type",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Medals");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Medals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "MedalsUsersLessons",
                columns: table => new
                {
                    MedalId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { 1, new DateTime(2020, 6, 1, 16, 27, 7, 172, DateTimeKind.Local), null, "Gold" });

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Type" },
                values: new object[] { 2, new DateTime(2020, 6, 1, 16, 27, 7, 173, DateTimeKind.Local).AddTicks(9468), null, "Silver" });

            migrationBuilder.InsertData(
                table: "Medals",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Type" },
                values: new object[] { 3, new DateTime(2020, 6, 1, 16, 27, 7, 173, DateTimeKind.Local).AddTicks(9508), null, "Bronze" });

            migrationBuilder.CreateIndex(
                name: "IX_MedalsUsersLessons_LessonId",
                table: "MedalsUsersLessons",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_MedalsUsersLessons_UserId",
                table: "MedalsUsersLessons",
                column: "UserId");
        }
    }
}
