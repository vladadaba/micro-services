﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_items",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_job_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_items",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    aggregate_type = table.Column<string>(maxLength: 255, nullable: false),
                    aggregate_id = table.Column<string>(maxLength: 255, nullable: false),
                    type = table.Column<string>(maxLength: 255, nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    correlation_id = table.Column<string>(nullable: false),
                    payload = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_items", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_items");

            migrationBuilder.DropTable(
                name: "outbox_items");
        }
    }
}
