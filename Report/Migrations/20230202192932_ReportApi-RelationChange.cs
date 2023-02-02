using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportApi.Migrations
{
    /// <inheritdoc />
    public partial class ReportApiRelationChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail",
                column: "ReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetail_ReportId",
                table: "ReportDetail",
                column: "ReportId",
                unique: true);
        }
    }
}
