using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class mc170203a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RiskLevel",
                table: "Event",
                maxLength: 10,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Likelihood",
                table: "Event",
                maxLength: 10,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Impact",
                table: "Event",
                maxLength: 10,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ActionBy",
                table: "Event",
                maxLength: 30,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RiskLevel",
                table: "Event",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Likelihood",
                table: "Event",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Impact",
                table: "Event",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ActionBy",
                table: "Event",
                maxLength: 10,
                nullable: false);
        }
    }
}
