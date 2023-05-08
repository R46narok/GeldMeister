using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankStatements.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialRmU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankStatements_Users_UserId",
                table: "BankStatements");

            migrationBuilder.DropIndex(
                name: "IX_BankStatements_UserId",
                table: "BankStatements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BankStatements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "BankStatements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankStatements_UserId",
                table: "BankStatements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankStatements_Users_UserId",
                table: "BankStatements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
