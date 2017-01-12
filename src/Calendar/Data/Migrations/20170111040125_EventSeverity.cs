using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class EventSeverity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Event",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Project",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }
    }
}
