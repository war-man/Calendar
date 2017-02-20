using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class ProjectStyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CalendarStyle",
                table: "Project",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Team",
                table: "TeamProject",
                maxLength: 15,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Project",
                table: "TeamProject",
                maxLength: 15,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Team",
                maxLength: 15,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                maxLength: 15,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                maxLength: 250,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Administrator",
                table: "Project",
                maxLength: 15,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalendarStyle",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Team",
                table: "TeamProject",
                maxLength: 5,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Project",
                table: "TeamProject",
                maxLength: 5,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Team",
                maxLength: 5,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                maxLength: 5,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Project",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Administrator",
                table: "Project",
                maxLength: 5,
                nullable: false);
        }
    }
}
