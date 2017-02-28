using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class mc170203bremovefieldseverity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "HealthCheckBy",
                table: "Event",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActionBy",
                table: "Event",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "HealthCheckBy",
                table: "Event",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ActionBy",
                table: "Event",
                maxLength: 30,
                nullable: false);
        }
    }
}
