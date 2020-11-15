using Microsoft.EntityFrameworkCore.Migrations;

namespace JobService.Migrations
{
    public partial class Jobs_Keyset_Pagination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "seq_jobs_number");

            migrationBuilder.AddColumn<int>(
                name: "serial_number",
                table: "job_items",
                nullable: false,
                defaultValueSql: "nextval('seq_jobs_number')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "seq_jobs_number");

            migrationBuilder.DropColumn(
                name: "serial_number",
                table: "job_items");
        }
    }
}
