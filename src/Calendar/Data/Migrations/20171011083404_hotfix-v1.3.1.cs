using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class hotfixv131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 300,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedHosts",
                table: "Event",
                maxLength: 500,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 200,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedHosts",
                table: "Event",
                maxLength: 150,
                nullable: false);
        }
    }
}
