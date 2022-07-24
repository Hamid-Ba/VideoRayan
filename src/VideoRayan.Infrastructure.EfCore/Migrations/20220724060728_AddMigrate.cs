using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoRayan.Infrastructure.EfCore.Migrations
{
    public partial class AddMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audience_Category_CategoryId",
                table: "Audience");

            migrationBuilder.DropForeignKey(
                name: "FK_Audience_Customers_UserId",
                table: "Audience");

            migrationBuilder.DropForeignKey(
                name: "FK_AudienceMeeting_Audience_MeetingId",
                table: "AudienceMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_AudienceMeeting_Meeting_AudienceId",
                table: "AudienceMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Customers_UserId",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudienceMeeting",
                table: "AudienceMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Audience",
                table: "Audience");

            migrationBuilder.RenameTable(
                name: "Meeting",
                newName: "Meetings");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "AudienceMeeting",
                newName: "AudienceMeetings");

            migrationBuilder.RenameTable(
                name: "Audience",
                newName: "Audiences");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_UserId",
                table: "Meetings",
                newName: "IX_Meetings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AudienceMeeting_AudienceId",
                table: "AudienceMeetings",
                newName: "IX_AudienceMeetings_AudienceId");

            migrationBuilder.RenameIndex(
                name: "IX_Audience_UserId",
                table: "Audiences",
                newName: "IX_Audiences_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Audience_CategoryId",
                table: "Audiences",
                newName: "IX_Audiences_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Meetings",
                type: "nvarchar(125)",
                maxLength: 125,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Audiences",
                type: "nvarchar(125)",
                maxLength: 125,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Audiences",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Audiences",
                type: "nvarchar(125)",
                maxLength: 125,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudienceMeetings",
                table: "AudienceMeetings",
                columns: new[] { "MeetingId", "AudienceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audiences",
                table: "Audiences",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CustomerId",
                table: "Categories",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceMeetings_Audiences_AudienceId",
                table: "AudienceMeetings",
                column: "AudienceId",
                principalTable: "Audiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceMeetings_Meetings_MeetingId",
                table: "AudienceMeetings",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audiences_Categories_CategoryId",
                table: "Audiences",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audiences_Customers_UserId",
                table: "Audiences",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Customers_CustomerId",
                table: "Categories",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Customers_UserId",
                table: "Meetings",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudienceMeetings_Audiences_AudienceId",
                table: "AudienceMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_AudienceMeetings_Meetings_MeetingId",
                table: "AudienceMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Audiences_Categories_CategoryId",
                table: "Audiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Audiences_Customers_UserId",
                table: "Audiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Customers_CustomerId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Customers_UserId",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CustomerId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Audiences",
                table: "Audiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AudienceMeetings",
                table: "AudienceMeetings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Meetings",
                newName: "Meeting");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Audiences",
                newName: "Audience");

            migrationBuilder.RenameTable(
                name: "AudienceMeetings",
                newName: "AudienceMeeting");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_UserId",
                table: "Meeting",
                newName: "IX_Meeting_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Audiences_UserId",
                table: "Audience",
                newName: "IX_Audience_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Audiences_CategoryId",
                table: "Audience",
                newName: "IX_Audience_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AudienceMeetings_AudienceId",
                table: "AudienceMeeting",
                newName: "IX_AudienceMeeting_AudienceId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Meeting",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(125)",
                oldMaxLength: 125);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(55)",
                oldMaxLength: 55);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Audience",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(125)",
                oldMaxLength: 125);

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Audience",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Audience",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(125)",
                oldMaxLength: 125);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audience",
                table: "Audience",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AudienceMeeting",
                table: "AudienceMeeting",
                columns: new[] { "MeetingId", "AudienceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Audience_Category_CategoryId",
                table: "Audience",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audience_Customers_UserId",
                table: "Audience",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceMeeting_Audience_MeetingId",
                table: "AudienceMeeting",
                column: "MeetingId",
                principalTable: "Audience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceMeeting_Meeting_AudienceId",
                table: "AudienceMeeting",
                column: "AudienceId",
                principalTable: "Meeting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Customers_UserId",
                table: "Meeting",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
