using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class affectedteams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffectedTeams",
                table: "Event",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectedTeams",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 10,
                nullable: true);
        }
    }
}
