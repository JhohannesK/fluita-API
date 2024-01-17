using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fb_API.Migrations
{
    /// <inheritdoc />
    public partial class resettokenexpiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpiration",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetTokenExpiration",
                table: "Users");
        }
    }
}
