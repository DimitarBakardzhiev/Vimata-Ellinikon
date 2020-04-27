using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vimata.Data.Migrations
{
    public partial class Exercises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lessons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClosedExercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Options = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    IsGreekContent = table.Column<bool>(nullable: false),
                    AreOptionsInGreek = table.Column<bool>(nullable: false),
                    IsHearingExercise = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosedExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DragAndDropExercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Pieces = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    IsGreekContent = table.Column<bool>(nullable: false),
                    ArePiecesInGreek = table.Column<bool>(nullable: false),
                    IsHearingExercise = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DragAndDropExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DragAndDropExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenExercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsGreekContent = table.Column<bool>(nullable: false),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    IsHearingExercise = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeakingExercises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    IsHearingExercise = table.Column<bool>(nullable: false),
                    LessonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakingExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakingExercises_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 687, DateTimeKind.Local).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 689, DateTimeKind.Local).AddTicks(5322));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 4, 17, 15, 12, 2, 689, DateTimeKind.Local).AddTicks(5364));

            migrationBuilder.CreateIndex(
                name: "IX_ClosedExercises_LessonId",
                table: "ClosedExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DragAndDropExercises_LessonId",
                table: "DragAndDropExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenExercises_LessonId",
                table: "OpenExercises",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakingExercises_LessonId",
                table: "SpeakingExercises",
                column: "LessonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosedExercises");

            migrationBuilder.DropTable(
                name: "DragAndDropExercises");

            migrationBuilder.DropTable(
                name: "OpenExercises");

            migrationBuilder.DropTable(
                name: "SpeakingExercises");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2020, 3, 9, 16, 55, 5, 163, DateTimeKind.Local).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2020, 3, 9, 16, 55, 5, 165, DateTimeKind.Local).AddTicks(1508));

            migrationBuilder.UpdateData(
                table: "Medals",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2020, 3, 9, 16, 55, 5, 165, DateTimeKind.Local).AddTicks(1547));
        }
    }
}
