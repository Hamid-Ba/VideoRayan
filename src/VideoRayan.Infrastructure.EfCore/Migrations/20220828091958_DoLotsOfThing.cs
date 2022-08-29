using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoRayan.Infrastructure.EfCore.Migrations
{
    public partial class DoLotsOfThing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MasterPinCode",
                table: "Meetings",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserPinCode",
                table: "Meetings",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MasterPinCode",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UserPinCode",
                table: "Meetings");
        }
    }
}
