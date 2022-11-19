using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogRotam.Data.Migrations
{
    public partial class addShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Shows",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shows",
                table: "Blogs");
        }
    }
}
