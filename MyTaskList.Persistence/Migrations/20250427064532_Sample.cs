using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { new Guid("8643806f-5759-48cb-9ce6-7802cb346b33"), new DateTime(2025, 4, 27, 2, 44, 0, 0, DateTimeKind.Utc), "Sample" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("8643806f-5759-48cb-9ce6-7802cb346b33"));
        }
    }
}
