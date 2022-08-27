using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoRayan.Infrastructure.EfCore.Migrations
{
    public partial class AddImageAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "FaceToFaces",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "FaceToFaces");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customers");
        }
    }
}
