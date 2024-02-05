using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class i6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "suffix",
                table: "t_resource",
                newName: "realPath");

            migrationBuilder.RenameColumn(
                name: "fileName",
                table: "t_resource",
                newName: "fileupName");

            migrationBuilder.AddColumn<string>(
                name: "contentType",
                table: "t_resource",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "fileExtention",
                table: "t_resource",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "fileOrginname",
                table: "t_resource",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contentType",
                table: "t_resource");

            migrationBuilder.DropColumn(
                name: "fileExtention",
                table: "t_resource");

            migrationBuilder.DropColumn(
                name: "fileOrginname",
                table: "t_resource");

            migrationBuilder.RenameColumn(
                name: "realPath",
                table: "t_resource",
                newName: "suffix");

            migrationBuilder.RenameColumn(
                name: "fileupName",
                table: "t_resource",
                newName: "fileName");
        }
    }
}
