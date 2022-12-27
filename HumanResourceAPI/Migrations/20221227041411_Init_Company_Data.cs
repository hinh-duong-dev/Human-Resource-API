using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HumanResourceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitCompanyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("c713a12b-2a6c-44a0-95ec-c6a81a3ab5c3"), "Redmond, Washington, U.S.", "USA", "Microsoft" },
                    { new Guid("eee826a8-4192-45dd-b039-02d8038c5b63"), "312 Forest Avenue, California", "USA", "Apple" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("c713a12b-2a6c-44a0-95ec-c6a81a3ab5c3"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("eee826a8-4192-45dd-b039-02d8038c5b63"));
        }
    }
}
