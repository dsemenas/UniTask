using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTask_backend.Migrations
{
    /// <inheritdoc />
    public partial class Task_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GroupId",
                table: "Tasks",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Groups_GroupId",
                table: "Tasks",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Groups_GroupId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_GroupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
