using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataAccess.Migrations
{
    public partial class logo_path : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logoImgPath",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "logo.png");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "id",
                keyValue: 1,
                column: "logoImgPath",
                value: "logo.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logoImgPath",
                table: "Settings");
        }
    }
}
