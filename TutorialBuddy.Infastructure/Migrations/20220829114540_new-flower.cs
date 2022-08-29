using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorBuddy.Infrastructure.Migrations
{
    public partial class newflower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComments_Sessions_SessionID",
                table: "StudentComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorComments_Sessions_SessiomID",
                table: "TutorComments");

            migrationBuilder.DropIndex(
                name: "IX_TutorComments_SessiomID",
                table: "TutorComments");

            migrationBuilder.DropIndex(
                name: "IX_StudentComments_SessionID",
                table: "StudentComments");

            migrationBuilder.RenameColumn(
                name: "SessiomID",
                table: "TutorComments",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "SessionID",
                table: "StudentComments",
                newName: "SessionId");

            migrationBuilder.AddColumn<int>(
                name: "RateStudent",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RateTutor",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TutorComments_SessionId",
                table: "TutorComments",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentComments_SessionId",
                table: "StudentComments",
                column: "SessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComments_Sessions_SessionId",
                table: "StudentComments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorComments_Sessions_SessionId",
                table: "TutorComments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentComments_Sessions_SessionId",
                table: "StudentComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorComments_Sessions_SessionId",
                table: "TutorComments");

            migrationBuilder.DropIndex(
                name: "IX_TutorComments_SessionId",
                table: "TutorComments");

            migrationBuilder.DropIndex(
                name: "IX_StudentComments_SessionId",
                table: "StudentComments");

            migrationBuilder.DropColumn(
                name: "RateStudent",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RateTutor",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "TutorComments",
                newName: "SessiomID");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "StudentComments",
                newName: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorComments_SessiomID",
                table: "TutorComments",
                column: "SessiomID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComments_SessionID",
                table: "StudentComments",
                column: "SessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentComments_Sessions_SessionID",
                table: "StudentComments",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorComments_Sessions_SessiomID",
                table: "TutorComments",
                column: "SessiomID",
                principalTable: "Sessions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
