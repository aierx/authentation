using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    public partial class i9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_user",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_spu_type",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_spu",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_role",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_resource",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDelete",
                table: "t_news",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_user");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_spu_type");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_spu");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_role");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_resource");

            migrationBuilder.DropColumn(
                name: "isDelete",
                table: "t_news");
        }
    }
}
