using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEFCourse.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalStatesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "States",
                column: "States",
                value: "On hold");
            migrationBuilder.InsertData(
                table: "States",
                column: "States",
                value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "States",
            keyColumn: "States",
            keyValue: "On hold");

            migrationBuilder.DeleteData(
            table: "States",
            keyColumn: "States",
            keyValue: "Rejected");
        }
    }
}
