using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class auditfieldsallownull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Event",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Event",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Event",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Event",
                maxLength: 50,
                nullable: false);
        }
    }
}
