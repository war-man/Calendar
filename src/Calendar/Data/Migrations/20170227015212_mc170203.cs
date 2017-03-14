using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class mc170203 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionBy",
                table: "Event",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "Event",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HealthCheckBy",
                table: "Event",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Likelihood",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RiskLevel",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TaskDescription",
                table: "Event",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Event",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Event",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedTeams",
                table: "Event",
                maxLength: 150,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 150,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedHosts",
                table: "Event",
                maxLength: 150,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionBy",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "HealthCheckBy",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Likelihood",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "RiskLevel",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "TaskDescription",
                table: "Event",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Event",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Event",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedTeams",
                table: "Event",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AffectedHosts",
                table: "Event",
                maxLength: 150,
                nullable: true);
        }
    }
}
