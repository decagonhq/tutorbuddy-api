using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorBuddy.Infrastructure.Migrations
{
    public partial class boom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "StudentComments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "StudentComments");
        }
    }
}
