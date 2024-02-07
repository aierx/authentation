using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class i8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "t_resource",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "t_resource",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_t_resource_Sort",
                table: "t_resource",
                column: "Sort");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_resource_Sort",
                table: "t_resource");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "t_resource");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "t_resource");
        }
    }
}
