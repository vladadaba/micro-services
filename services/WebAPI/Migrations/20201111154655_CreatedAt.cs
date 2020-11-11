using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class CreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OutboxItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OutboxItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "JobItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OutboxItems");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "JobItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OutboxItems",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobItems",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
